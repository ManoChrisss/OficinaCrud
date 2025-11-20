using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oficina.Models;
using Microsoft.EntityFrameworkCore;

namespace oficina.Models.Repositories
{
    public class repositoryVeiculo : IDisposable
    {
        private OficinaDBContext db;

        public repositoryVeiculo()
        {
            db = new OficinaDBContext();
        }

        public Veiculo Incluir(Veiculo oVeiculo)
        {
            db.Add(oVeiculo);
            db.SaveChanges();
            return oVeiculo;
        }

        public void Alterar(Veiculo oVeiculo)
        {
            var veiculoBanco = db.Veiculos.FirstOrDefault(v => v.VeiculoId == oVeiculo.VeiculoId);

            if (veiculoBanco == null)
                throw new Exception("Veículo não encontrado para alteração.");

            // Atualiza apenas os campos desejados
            veiculoBanco.ClienteId = oVeiculo.ClienteId;
            veiculoBanco.Marca = oVeiculo.Marca;
            veiculoBanco.Modelo = oVeiculo.Modelo;
            veiculoBanco.Ano = oVeiculo.Ano;
            veiculoBanco.Placa = oVeiculo.Placa;

            db.SaveChanges();
        }


        public bool Excluir(int id)
        {
            var veiculo = db.Veiculos
                            .Include(v => v.OrdensServicos)
                            .FirstOrDefault(v => v.VeiculoId == id);

            if (veiculo == null)
                return false;

            if (veiculo.OrdensServicos.Any())
                return false; 

            db.Veiculos.Remove(veiculo);
            db.SaveChanges();
            return true;
        }




        public Veiculo? Selecionar(int id)
        {
            return db.Veiculos
                     .Include(v => v.Cliente)
                     .FirstOrDefault(v => v.VeiculoId == id);
        }

        public List<Veiculo> SelecionarTodos()
        {
            return db.Veiculos
                     .Include(v => v.Cliente)
                     .OrderBy(v => v.VeiculoId)
                     .ToList();
        }

        // Apenas os clientes sem veículos cadastrados
        public List<Cliente> SelecionarClientesSemVeiculos()
        {
            return db.Clientes
                     .Where(c => !c.Veiculos.Any())
                     .OrderBy(c => c.Nome)
                     .ToList();
        }

        public List<Cliente> SelecionarTodosClientes()
        {
            return db.Clientes.OrderBy(c => c.Nome).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }


    }
}

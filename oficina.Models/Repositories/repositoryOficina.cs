using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oficina.Models.Repositories
{
    public class repositoryOficina: IDisposable
    {
        private OficinaDBContext db;

        public repositoryOficina()
        {
            db = new OficinaDBContext();
        }


        public Cliente Incluir(Cliente oCliente)
        {
            db.Add(oCliente);
            db.SaveChanges();
            return oCliente;
        }

        public void Alterar(Cliente oCliente)
        {
            db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public bool Excluir(int id)
        {
            // Busca o cliente incluindo os veículos
            var cliente = db.Clientes
                            .Include(c => c.Veiculos)
                            .FirstOrDefault(c => c.ClienteId == id);

            if (cliente == null)
                return false; // Cliente não encontrado

            // Verifica se há veículos associados
            if (cliente.Veiculos.Any())
                return false; // Não pode excluir

            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return true; // Exclusão realizada
        }



        public Cliente? Selecionar(int id)
        {
            return db.Clientes.Find(id);
        }

        public List<Cliente> SelecionarTodos()
        {
            return (from p in db.Clientes
                    orderby p.ClienteId
                    select p).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

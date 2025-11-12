using oficina.Models;
using oficina.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oficina.View.Controllers
{
    public class CVeiculo : IDisposable
    {
        repositoryVeiculo _Repository;

        public CVeiculo()
        {
            _Repository = new repositoryVeiculo();
        }

        public List<Veiculo> SelecionarTodos()
        {
            return _Repository.SelecionarTodos();
        }

        public Veiculo Incluir(Veiculo veiculo)
        {
            return _Repository.Incluir(veiculo);
        }

        public Veiculo? Selecionar(int id)
        {
            return _Repository.Selecionar(id);
        }

        public void Alterar(Veiculo veiculo)
        {
            _Repository.Alterar(veiculo);
        }

        public bool Excluir(int id)
        {
            return _Repository.Excluir(id);
        }


        // 🔹 Método pra listar clientes que ainda não possuem veículo
        public List<Cliente> SelecionarClientesSemVeiculos()
        {
            return _Repository.SelecionarClientesSemVeiculos();
        }

        public List<Cliente> SelecionarTodosClientes()
        {
            return _Repository.SelecionarTodosClientes();
        }

        public void Dispose()
        {
            _Repository.Dispose();
        }
    }
}

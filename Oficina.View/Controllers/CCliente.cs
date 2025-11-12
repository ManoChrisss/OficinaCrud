using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oficina.Models;
using oficina.Models.Repositories;

namespace Oficina.View.Controllers
{
    public class CCliente : IDisposable
    {
        repositoryOficina _Repository;

        public CCliente()
        {
            _Repository = new repositoryOficina();
        }

        public List<Cliente> SelecionarTodos()
        {
            return _Repository.SelecionarTodos();
        }

        public Cliente Incluir(Cliente cliente)
        {
            return _Repository.Incluir(cliente);
        }

        public Cliente? Selecionar(int id)
        {
            return _Repository.Selecionar(id);
        }

        public void Alterar(Cliente cliente)
        {
            _Repository.Alterar(cliente);
        }

        // 🔹 Adicionado método Excluir
        public bool Excluir(int id)
        {
            return _Repository.Excluir(id);
        }

        public void Dispose()
        {
            _Repository.Dispose();
        }
    }
}

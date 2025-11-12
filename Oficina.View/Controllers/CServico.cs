using System;
using System.Collections.Generic;
using oficina.Models;
using oficina.Models.Repositories;

namespace Oficina.View.Controllers
{
    public class CServico : IDisposable
    {
        private readonly ServicoRepository _repo;

        public CServico()
        {
            _repo = new ServicoRepository();
        }

        public Servico Incluir(Servico oServico)
        {
            return _repo.Incluir(oServico);
        }

        public void Alterar(Servico oServico)
        {
            _repo.Alterar(oServico);
        }

        public bool Excluir(int id)
        {
            return _repo.Excluir(id); // _Repository.Excluir também deve retornar bool
        }


        public Servico? Selecionar(int id)
        {
            return _repo.Selecionar(id);
        }

        public List<Servico> SelecionarTodos()
        {
            return _repo.SelecionarTodos();
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}

using oficina.Models;
using oficina.Models.Repositories;
using System;
using System.Collections.Generic;

namespace Oficina.View.Controllers
{
    public class COrdemServico : IDisposable
    {
        private readonly OrdemServicoRepository _repo;

        public COrdemServico()
        {
            _repo = new OrdemServicoRepository();
        }

        public List<dynamic> SelecionarRelatorio()
        {
            return _repo.SelecionarRelatorio();
        }

        public OrdensServico Incluir(OrdensServico ordem)
        {
            return _repo.Incluir(ordem);
        }

        // Excluir ordem de serviço
        public bool Excluir(int id)
        {
            try
            {
                _repo.Excluir(id);
                return true;
            }
            catch
            {
                // Aqui pode retornar false se houver restrição de FK ou outro erro
                return false;
            }
        }

        // Alterar apenas a observação da ordem
        public void AlterarObservacao(int id, string observacao)
        {
            var ordem = _repo.Selecionar(id);
            if (ordem != null)
            {
                ordem.Observacoes = observacao;
                _repo.Alterar(ordem);
            }
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}

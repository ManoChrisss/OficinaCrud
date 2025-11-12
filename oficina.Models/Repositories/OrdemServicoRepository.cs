using System;
using System.Collections.Generic;
using System.Linq;
using oficina.Models;

namespace oficina.Models.Repositories
{
    public class OrdemServicoRepository : IDisposable
    {
        private OficinaDBContext db;

        public OrdemServicoRepository()
        {
            db = new OficinaDBContext();
        }

        public OrdensServico Incluir(OrdensServico ordem)
        {
            db.Add(ordem);
            db.SaveChanges();
            return ordem;
        }

        public void Alterar(OrdensServico ordem)
        {
            db.Entry(ordem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public void Excluir(int id)
        {
            var ordem = db.OrdensServicos.Find(id);
            if (ordem != null)
            {
                db.OrdensServicos.Remove(ordem);
                db.SaveChanges();
            }
        }

        public OrdensServico? Selecionar(int id)
        {
            return db.OrdensServicos.Find(id);
        }

        public List<OrdensServico> SelecionarTodos()
        {
            return db.OrdensServicos
                     .OrderBy(o => o.OrdemId)
                     .ToList();
        }

        // Para relatórios: retorna lista unindo Cliente, Veículo e Serviço
        public List<dynamic> SelecionarRelatorio()
        {
            var query = from o in db.OrdensServicos
                        join c in db.Clientes on o.ClienteId equals c.ClienteId
                        join v in db.Veiculos on o.VeiculoId equals v.VeiculoId
                        join s in db.Servicos on o.ServicoId equals s.ServicoId
                        select new
                        {
                            o.OrdemId,
                            Cliente = c.Nome,
                            Veiculo = $"{v.Marca} {v.Modelo} ({v.Placa})",
                            Servico = s.Descricao,
                            s.Preco,
                            o.DataServico,
                            o.Observacoes
                        };

            return query.ToList<dynamic>();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

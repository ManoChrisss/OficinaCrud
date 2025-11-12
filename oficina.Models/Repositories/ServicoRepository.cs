using System;
using System.Collections.Generic;
using System.Linq;
using oficina.Models;
using Microsoft.EntityFrameworkCore;

namespace oficina.Models.Repositories
{
    public class ServicoRepository : IDisposable
    {
        private OficinaDBContext db;

        public ServicoRepository()
        {
            db = new OficinaDBContext();
        }

        public Servico Incluir(Servico oServico)
        {
            db.Add(oServico);
            db.SaveChanges();
            return oServico;
        }

        public void Alterar(Servico oServico)
        {
            db.Entry(oServico).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool Excluir(int id)
        {
            var srv = db.Servicos
                        .Include(s => s.OrdensServicos)
                        .FirstOrDefault(s => s.ServicoId == id);

            if (srv == null || (srv.OrdensServicos != null && srv.OrdensServicos.Any()))
                return false;

            db.Servicos.Remove(srv);
            db.SaveChanges();
            return true;
        }



        public Servico? Selecionar(int id)
        {
            return db.Servicos.Find(id);
        }

        public List<Servico> SelecionarTodos()
        {
            return (from s in db.Servicos
                    orderby s.ServicoId
                    select s).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

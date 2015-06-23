using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTcc.Entity;
using System.Data.Objects;

namespace ProjetoTcc.Data
{
    class CidadeData
    {
        public keite_modasEntities db;
        private ObjectSet<cidade> cidades;

        public CidadeData(keite_modasEntities _db)
        {
            db = _db;
            cidades = db.CreateObjectSet<cidade>();
        }

        public List<cidade> todasCidades()
        {
            return cidades.ToList();
        }

        public string excluirCidade(cidade cidade)
        {
            string erro = null;
            try
            {
                cidades.DeleteObject(cidade);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public cidade obterCidade(int id)
        {
            var lista = from c in cidades where c.IdCidade == id select c;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarCidade(cidade cidade)
        {
            string erro = null;
            try
            {
                cidades.AddObject(cidade);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarCidade(cidade cidade)
        {
            string erro = null;
            try
            {
                if (cidade.EntityState == System.Data.EntityState.Detached)
                {
                    cidades.Attach(cidade);
                }
                db.ObjectStateManager.ChangeObjectState(cidade, System.Data.EntityState.Modified);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTcc.Entity;
using System.Data.Objects;

namespace ProjetoTcc.Data
{
    class UfData
    {
        public keite_modasEntities db;
        private ObjectSet<uf> ufs;

        public UfData(keite_modasEntities _db)
        {
            db = _db;
            ufs = db.CreateObjectSet<uf>();
        }

        public List<uf> todasUfs()
        {
            return ufs.ToList();
        }

        public string excluirUf(uf uf)
        {
            string erro = null;
            try
            {
                ufs.DeleteObject(uf);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public uf obterUf(string uf)
        {
            var lista = from u in ufs where u.UF == uf select u;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarUf(uf uf)
        {
            string erro = null;
            try
            {
                ufs.AddObject(uf);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarUf(uf uf)
        {
            string erro = null;
            try
            {
                if (uf.EntityState == System.Data.EntityState.Detached)
                {
                    ufs.Attach(uf);
                }
                db.ObjectStateManager.ChangeObjectState(uf, System.Data.EntityState.Modified);

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

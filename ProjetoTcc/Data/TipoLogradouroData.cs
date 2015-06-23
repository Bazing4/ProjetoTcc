using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using ProjetoTcc.Entity;

namespace ProjetoTcc.Data
{
    class TipoLogradouroData
    {
        public keite_modasEntities db;
        private ObjectSet<tipo_logradouro> tipoLogradouros;

        public TipoLogradouroData(keite_modasEntities _db)
        {
            db = _db;
            tipoLogradouros = db.CreateObjectSet<tipo_logradouro>();
        }

        public List<tipo_logradouro> todosTipoLogradouros()
        {
            return tipoLogradouros.ToList();
        }

        public string excluirTipoLogradouro(tipo_logradouro tipoLogradouro)
        {
            string erro = null;
            try
            {
                tipoLogradouros.DeleteObject(tipoLogradouro);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public tipo_logradouro obterTipoLogradouro(int id)
        {
            var lista = from t in tipoLogradouros where t.IdLogradouro == id select t;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarTipoLogradouro(tipo_logradouro tipoLogradouro)
        {
            string erro = null;
            try
            {
                tipoLogradouros.AddObject(tipoLogradouro);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarTipoLogradouro(tipo_logradouro tipoLogradouro)
        {
            string erro = null;
            try
            {
                if (tipoLogradouro.EntityState == System.Data.EntityState.Detached)
                {
                    tipoLogradouros.Attach(tipoLogradouro);
                }
                db.ObjectStateManager.ChangeObjectState(tipoLogradouro, System.Data.EntityState.Modified);

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

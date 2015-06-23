using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTcc.Entity;
using System.Data.Objects;

namespace ProjetoTcc.Data
{
    class TipoTelefoneData
    {
        public keite_modasEntities db;
        private ObjectSet<tipo_telefone> tipoTelefones;

        public TipoTelefoneData(keite_modasEntities _db)
        {
            db = _db;
            tipoTelefones = db.CreateObjectSet<tipo_telefone>();
        }

        public List<tipo_telefone> todosTipoTelefones()
        {
            return tipoTelefones.ToList();
        }

        public string excluirTipoTelefone(tipo_telefone tipoTelefone)
        {
            string erro = null;
            try
            {
                tipoTelefones.DeleteObject(tipoTelefone);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public tipo_telefone obterTipoTelefone(int id)
        {
            var lista = from t in tipoTelefones where t.IdTipoTelefone == id select t;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarTipoTelefone(tipo_telefone tipoTelefone)
        {
            string erro = null;
            try
            {
                tipoTelefones.AddObject(tipoTelefone);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarTipoTelefone(tipo_telefone tipoTelefone)
        {
            string erro = null;
            try
            {
                if (tipoTelefone.EntityState == System.Data.EntityState.Detached)
                {
                    tipoTelefones.Attach(tipoTelefone);
                }
                db.ObjectStateManager.ChangeObjectState(tipoTelefone, System.Data.EntityState.Modified);

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

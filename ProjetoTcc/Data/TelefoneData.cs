using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTcc.Entity;
using System.Data.Objects;

namespace ProjetoTcc.Data
{
    class TelefoneData
    {
        public keite_modasEntities db;
        private ObjectSet<telefone> telefones;

        public TelefoneData(keite_modasEntities _db)
        {
            db = _db;
            telefones = db.CreateObjectSet<telefone>();
        }

        public List<telefone> todosTelefones()
        {
            return telefones.ToList();
        }

        public string excluirTelefone(telefone telefone)
        {
            string erro = null;
            try
            {
                telefones.DeleteObject(telefone);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public telefone obterTelefone(int id)
        {
            var lista = from t in telefones where t.IdPessoa == id select t;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarTelefone(telefone telefone)
        {
            string erro = null;
            try
            {
                telefones.AddObject(telefone);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarTelefone(telefone telefone)
        {
            string erro = null;
            try
            {
                if (telefone.EntityState == System.Data.EntityState.Detached)
                {
                    telefones.Attach(telefone);
                }
                db.ObjectStateManager.ChangeObjectState(telefone, System.Data.EntityState.Modified);

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

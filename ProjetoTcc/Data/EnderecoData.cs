using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTcc.Entity;
using System.Data.Objects;

namespace ProjetoTcc.Data
{
    class EnderecoData
    {
        public keite_modasEntities1 db;
        private ObjectSet<endereco> enderecos;

        public EnderecoData(keite_modasEntities1 _db)
        {
            db = _db;
            enderecos = db.CreateObjectSet<endereco>();
        }

        public List<endereco> todosEnderecos()
        {
            return enderecos.ToList();
        }

        public string excluirEndereco(endereco endereco)
        {
            string erro = null;
            try
            {
                enderecos.DeleteObject(endereco);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public endereco obterEndereco(int id)
        {
            var lista = from e in enderecos where e.IdPessoa == id select e;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarEndereco(endereco endereco)
        {
            string erro = null;
            try
            {
                enderecos.AddObject(endereco);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarEndereco(endereco endereco)
        {
            string erro = null;
            try
            {
                if (endereco.EntityState == System.Data.EntityState.Detached)
                {
                    enderecos.Attach(endereco);
                }
                db.ObjectStateManager.ChangeObjectState(endereco, System.Data.EntityState.Modified);

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

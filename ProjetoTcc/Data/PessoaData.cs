using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTcc.Entity;
using System.Data.Objects;

namespace ProjetoTcc.Data
{
    class PessoaData
    {
        public keite_modasEntities1 db;
        private ObjectSet<pessoa> pessoas;

        public PessoaData(keite_modasEntities1 _db)
        {
            db = _db;
            pessoas = db.CreateObjectSet<pessoa>();
        }

        public List<pessoa> todasPessoas()
        {
            return pessoas.ToList();
        }

        public string excluirPessoa(pessoa pessoa)
        {
            string erro = null;
            try
            {
                pessoas.DeleteObject(pessoa);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public pessoa obterPessoa(int id)
        {
            var lista = from p in pessoas where p.IdPessoa == id select p;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarPessoa(pessoa pessoa)
        {
            string erro = null;
            try
            {
                pessoas.AddObject(pessoa);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarPessoa(pessoa pessoa)
        {
            string erro = null;
            try
            {
                if (pessoa.EntityState == System.Data.EntityState.Detached)
                {
                    pessoas.Attach(pessoa);
                }
                db.ObjectStateManager.ChangeObjectState(pessoa, System.Data.EntityState.Modified);

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

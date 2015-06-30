using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTcc.Entity;
using System.Data.Objects;

namespace ProjetoTcc.Data
{
    class ProdutoData
    {
        public keite_modasEntities1 db;
        private ObjectSet<produto> produtos;

        public ProdutoData(keite_modasEntities1 _db)
        {
            db = _db;
            produtos = db.CreateObjectSet<produto>();
        }

        public List<produto> todosProdutos()
        {
            return produtos.ToList();
        }

        public string excluirProduto(produto produto)
        {
            string erro = null;
            try
            {
                produtos.DeleteObject(produto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public produto obterProduto(int id)
        {
            var lista = from p in produtos where p.IdProduto == id select p;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarProduto(produto produto)
        {
            string erro = null;
            try
            {
                produtos.AddObject(produto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarProduto(produto produto)
        {
            string erro = null;
            try
            {
                if (produto.EntityState == System.Data.EntityState.Detached)
                {
                    produtos.Attach(produto);
                }
                db.ObjectStateManager.ChangeObjectState(produto, System.Data.EntityState.Modified);

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

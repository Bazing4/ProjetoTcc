using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetoTcc.Entity;
using System.Data.Objects;

namespace ProjetoTcc.Data
{
    class TipoProdutoData
    {
        public keite_modasEntities1 db;
        private ObjectSet<tipoproduto> tipoProdutos;

        public TipoProdutoData(keite_modasEntities1 _db)
        {
            db = _db;
            tipoProdutos = db.CreateObjectSet<tipoproduto>();
        }

        public List<tipoproduto> todosTiposProdutos()
        {
            return tipoProdutos.ToList();
        }

        public string excluirTipoProduto(tipoproduto tipoProduto)
        {
            string erro = null;
            try
            {
                tipoProdutos.DeleteObject(tipoProduto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public tipoproduto obterTipoProduto(int id)
        {
            var lista = from p in tipoProdutos where p.IdTipoProduto == id select p;
            return lista.ToList().FirstOrDefault();
        }

        public string adicionarTipoProduto(tipoproduto tipoproduto)
        {
            string erro = null;
            try
            {
                tipoProdutos.AddObject(tipoproduto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return erro;
        }

        public string editarTipoProduto(tipoproduto tipoproduto)
        {
            string erro = null;
            try
            {
                if (tipoproduto.EntityState == System.Data.EntityState.Detached)
                {
                    tipoProdutos.Attach(tipoproduto);
                }
                db.ObjectStateManager.ChangeObjectState(tipoproduto, System.Data.EntityState.Modified);

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

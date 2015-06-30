using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoTcc.Entity;
using ProjetoTcc.Data;

namespace ProjetoTcc.Views.Produto
{
    public partial class FormProduto : Form
    {
        private keite_modasEntities1 db;

        private produto produto;
        private ProdutoData produtoData;
        private TipoProdutoData tipoProdutoData;


        public FormProduto()
        {
            InitializeComponent();

            db = new keite_modasEntities1();

            produtoData = new ProdutoData(db);
            tipoProdutoData = new TipoProdutoData(db);
            produto = new produto();

            inicializar();
        }
        private void inicializar()
        {
            resetarCampos();
            inicializarDataGridView();
            inicializarCbx();
        }
        private void inicializarCbx()
        {
            cbxTipo.DataSource = tipoProdutoData.todosTiposProdutos();
            cbxTipo.DisplayMember = "descricao";
            cbxTipo.ValueMember = "idTipoProduto";

        }
        private void inicializarDataGridView()
        {
            dgvProduto.DataSource = produtoData.todosProdutos();
            dgvProduto.Columns[0].Visible = false;
            dgvProduto.Columns[1].HeaderText = "Preço";
            dgvProduto.Columns[2].HeaderText = "Fornecedor";
            dgvProduto.Columns[3].HeaderText = "Descrição";
            dgvProduto.Columns[4].HeaderText = "Preço de Venda";
            dgvProduto.Columns[5].HeaderText = "Observação";
            dgvProduto.Columns[6].HeaderText = "Tipo";
            dgvProduto.Columns[7].Visible = false;
            dgvProduto.Columns[8].Visible = false;
            dgvProduto.Columns[9].Visible = false;
        }
        private bool validarCampos()
        {
            if (txtDesc.Text == "" || txtDesc.Text == null)
            {
                MessageBox.Show("Descrição Invalida!");
                return false;
            }
            if (txtFornecedor.Text == "" || txtFornecedor.Text == null)
            {
                MessageBox.Show("Forncedor Invalido!");
                return false;
            }
            if (mtxPreco.Text == "" || mtxPreco.Text == null)
            {
                MessageBox.Show("Preço Invalido!");
                return false;

            }
            if (mtxPrecoVenda.Text == "" || mtxPrecoVenda.Text == null)
            {
                MessageBox.Show("Preço Invalido!");
                return false;
            }

            return true;
        }
        private void obterProduto()
        {
            produto.Descricao = txtDesc.Text;
            produto.Fornecedor = txtFornecedor.Text;
            produto.Preco = Convert.ToInt32(mtxPreco.Text);
            produto.PrecoVenda = Convert.ToInt32(mtxPrecoVenda.Text);
            produto.IdTipoProduto = (short)cbxTipo.SelectedValue;
            produto.Observacao = rtxtObserv.Text;
        }
        private void resetarCampos()
        {
            produto = new produto();
            txtDesc.Text = "";
            txtFornecedor.Text = "";
            mtxPrecoVenda.Text = "";
            mtxPreco.Text = "";
            rtxtObserv.Text = "";
        }
        private produto getProdutoSelecionado()
        {
            DataGridViewRow p = dgvProduto.CurrentRow;
            if (p != null)
                return (produto)p.DataBoundItem;
            return null;
        }

        public void atualizarTabela()
        {
            var lista = from p in produtoData.todosProdutos()
                        where p.Descricao.ToLower().Contains(txtPesquisar.Text.ToLower())
                        select p;
            dgvProduto.DataSource = lista.ToList();
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            resetarCampos();
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            string erro = produtoData.excluirProduto(getProdutoSelecionado());

            if (erro == null)
            {
                MessageBox.Show("Produto excluido com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao excluir o produto.");
            }
            inicializar();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            produto = getProdutoSelecionado();
            if (produto != null)
            {
                txtDesc.Text = produto.Descricao;
                txtFornecedor.Text = produto.Fornecedor;
                mtxPreco.Text = produto.Preco.ToString();
                mtxPrecoVenda.Text = produto.PrecoVenda.ToString();
                cbxTipo.SelectedValue = produto.tipoproduto.IdTipoProduto;
                rtxtObserv.Text = produto.Observacao;
            }
            else
            {
                MessageBox.Show("Nenhum produto selecionado!");
            }
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (!validarCampos())
            {
                return;
            }
            obterProduto();
            string erro = null;
            if (produto.IdProduto == 0)
            {

                erro = produtoData.adicionarProduto(produto);
            }
            else
            {
                erro = produtoData.editarProduto(produto);
            }
            if (erro == null)
            {
                MessageBox.Show("Produto salvo com sucesso!");
                atualizarTabela();
                resetarCampos();
            }
            else
            {
                MessageBox.Show("Erro ao salvar o produto." + erro);
            }
        }

        private void txtPesquisar_TextChanged_1(object sender, EventArgs e)
        {
            atualizarTabela();
        }

    }
}

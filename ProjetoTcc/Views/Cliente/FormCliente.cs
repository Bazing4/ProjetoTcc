using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoTcc.Data;
using ProjetoTcc.Entity;

namespace ProjetoTcc.Views.Cliente
{
    public partial class FormCliente : Form
    {
        private keite_modasEntities db;
        private pessoa pessoa;
        private TelefoneData telefoneData;
        private PessoaData pessoaData;
        private EnderecoData enderecoData;
        private TipoLogradouroData tipoLogradouroData;
        private CidadeData cidadeData;


        public FormCliente()
        {
            InitializeComponent();
            /*pessoa p = new pessoa();
            foreach (string telefone in listBox1.Items)
            {
                telefone t = new telefone();
                t.Numero = telefone;
                p.telefones.add(t);
            }*/
            db = new keite_modasEntities();
            pessoaData = new PessoaData(db);
            enderecoData = new EnderecoData(db);
            telefoneData = new TelefoneData(db);
            cidadeData = new CidadeData(db);
            tipoLogradouroData = new TipoLogradouroData(db);

            pessoa = new pessoa();
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
            cbxCidade.DataSource = cidadeData.todasCidades();
            cbxCidade.DisplayMember = "nomeCidade";
            cbxCidade.ValueMember = "idCidade";

            cbxTipoLogradouro.DataSource = tipoLogradouroData.todosTipoLogradouros();
            cbxTipoLogradouro.DisplayMember = "descricao";
            cbxTipoLogradouro.ValueMember = "idLogradouro";

        }
        private void inicializarDataGridView()
        {
            atualizarTabela();
        }
        private bool validarCampos()
        {
            if (txtBairro.Text == "" || txtBairro.Text == null)
            {
                MessageBox.Show("Bairro Invalido!");
                return false;
            }

            if (txtLogradouro.Text == "" || txtLogradouro.Text == null)
            {
                MessageBox.Show("Logradouro Invalido!");
                return false;
            }

            if (txtNome.Text == "" || txtNome.Text == null)
            {
                MessageBox.Show("Nome Invalido!");
                return false;
            }

            if (txtNumero.Text == "" || txtNumero.Text == null)
            {
                MessageBox.Show("Número Invalido!");
                return false;
            }

            if (txtTel.Text == "" || txtTel.Text == null)
            {
                MessageBox.Show("Número Invalido!");
                return false;
            }

            if (mtxCPF.Text == "" || txtNome.Text == null)
            {
                MessageBox.Show("CPF Invalido!");
                return false;
            }

            return true;
        }
        private void obterPessoa()
        {
            pessoa.Nome = txtNome.Text;
            pessoa.CPF = (mtxCPF.Text.ToString()); //pessoa
            pessoa.Observacao = rtxtObservacao.Text;

            pessoa.endereco.Bairro = txtBairro.Text;
            pessoa.endereco.cidade.NomeCidade = (cbxCidade.SelectedValue.ToString());
            pessoa.endereco.Logradouro = txtLogradouro.Text;
            pessoa.endereco.Numero = Convert.ToInt16(txtNumero.Text);
            pessoa.endereco.tipo_logradouro.Descricao = (cbxTipoLogradouro.SelectedValue.ToString()); //endereco
            pessoa.telefone.Numero = Convert.ToInt32(txtTel.Text);

            
        }
        private void resetarCampos()
        {
            pessoa = new pessoa();
            txtTel.Text = "";
            txtPesquisar.Text = "";
            txtNumero.Text = "";
            txtNome.Text = "";
            txtLogradouro.Text = "";
            txtBairro.Text = "";
            rtxtObservacao.Text = "";
            mtxCPF.Text = "";
        }
        private pessoa getPessoaSelecionada()
        {
            DataGridViewRow p = dgvClientes.CurrentRow;
            if (p != null)
                return (pessoa)p.DataBoundItem;
            return null;
        }

        public void atualizarTabela()
        {
            var lista = from c in cidadeData.todasCidades()
                        join e in enderecoData.todosEnderecos()
                        on c.IdCidade equals e.IdCidade      
                        join pe in pessoaData.todasPessoas()
                        on e.IdPessoa equals pe.IdPessoa
                        join t in telefoneData.todosTelefones()
                        on pe.IdPessoa equals t.IdPessoa

                        where pe.Nome.ToLower().Contains(txtPesquisar.Text.ToLower())
                        select new
                        {
                            nome = pe.Nome,
                            cpf = pe.CPF,
                            observacao= pe.Observacao,
                            endereco=e.Logradouro,
                            numeroEndereco =e.Numero,
                            bairro=e.Bairro,
                            cidade=c.NomeCidade,
                            telefone=t.Numero,

                        };

            dgvClientes.DataSource = lista.ToList();
            dgvClientes.Columns[0].HeaderText = "Nome";
            dgvClientes.Columns[1].HeaderText = "CPF";
            dgvClientes.Columns[2].HeaderText = "Observação"; 
            dgvClientes.Columns[3].HeaderText = "Endereço";
            dgvClientes.Columns[4].HeaderText = "Número";
            dgvClientes.Columns[5].HeaderText = "Bairro";
            dgvClientes.Columns[6].HeaderText = "Cidade";
            dgvClientes.Columns[7].HeaderText = "Telefone";
        }


        private void btnNovo_Click(object sender, EventArgs e)
        {
            resetarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            pessoa = getPessoaSelecionada();
            if (pessoa != null)
            {
                txtNome.Text = pessoa.Nome;
                mtxCPF.Text = pessoa.CPF;
                rtxtObservacao.Text = pessoa.Observacao;

                txtBairro.Text = pessoa.endereco.Bairro;
                txtLogradouro.Text = pessoa.endereco.Logradouro;
                txtNumero.Text = pessoa.endereco.Numero.ToString();
                txtTel.Text = pessoa.telefone.Numero.ToString();
                cbxCidade.SelectedValue = pessoa.endereco.cidade.NomeCidade;
                cbxTipoLogradouro.SelectedValue = pessoa.endereco.tipo_logradouro.Descricao;

            }
            else
            {
                MessageBox.Show("Nenhum produto selecionado!");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string erro = pessoaData.excluirPessoa(getPessoaSelecionada());

            if (erro == null)
            {
                MessageBox.Show("Pessoa excluida com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao excluir pessoa.");
            }
            inicializar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!validarCampos())
            {
                return;
            }
            obterPessoa();
            string erro = null;
            if (pessoa.IdPessoa == 0)
            {

                erro = pessoaData.adicionarPessoa(pessoa);
            }
            else
            {
                erro = pessoaData.editarPessoa(pessoa);
            }
            if (erro == null)
            {
                MessageBox.Show("Pessoa salva com sucesso!");
                atualizarTabela();
                resetarCampos();
            }
            else
            {
                MessageBox.Show("Erro ao salvar o produto." + erro);
            }
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            atualizarTabela();
        }



    }
}

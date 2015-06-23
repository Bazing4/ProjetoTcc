using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoTcc.Views.Produto;
using ProjetoTcc.Views.Cliente;

namespace ProjetoTcc
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormProduto().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormCliente().Show();
        }
    }
}

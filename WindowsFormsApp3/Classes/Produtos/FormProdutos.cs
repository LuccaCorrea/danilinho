using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3.Classes.Produtos
{
    public partial class FormProdutos : Form
    {
        private bool ehEdicao = false;
        public FormProdutos(int CI = 0, bool ehConsulta = false)
        {
            InitializeComponent();
            if (CI != 0)
            {
                try
                {
                    if (ehConsulta)
                    {
                        btnCadastrar.Visible = false;
                        lblTitulo.Text = "Cadastro de Produtos - Consulta";
                    }
                    else
                    {
                        lblTitulo.Text = "Cadastro de Produtos - Alteração";
                    }

                    ehEdicao = true;
                    Produtos produtos = new Produtos();
                    Produtos dados = produtos.getProdutoPorCI(CI);
                    txtCI.Text = dados.CI.ToString();
                    txtSKU.Text = dados.SKU;
                    txtDescricao.Text = dados.Descricao;
                    txtLote.Text = dados.Lote;
                    txtTipoBebida.Text = dados.TipoBebida;
                    txtTipoGarrafa.Text = dados.TipoGarrafa;
                    txtEnd.Text = dados.Estoque.End;
                    txtQtd.Text = dados.Estoque.Qtd;
                    txtFabricante.Text = dados.Estoque.Fabricante;
                    txtValorUnitario.Text = dados.Estoque.ValorUnitario;
                    txtContemCaixa.Text = dados.Estoque.ContemCaixa;
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }

            }
            else
            {
                ehEdicao = false;
                lblTitulo.Text = "Cadastro de Produtos - Inclusão";
                txtCI.Enabled = false;
                txtSKU.Focus();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Produtos produtos = new Produtos(txtSKU.Text, txtDescricao.Text,
                               txtLote.Text, txtTipoBebida.Text, txtTipoGarrafa.Text,
                               new Estoque(txtEnd.Text, txtQtd.Text, txtFabricante.Text, txtValorUnitario.Text, txtContemCaixa.Text));

                if (ehEdicao == true)
                {
                    produtos.CI = int.Parse(txtCI.Text.ToString());
                    //UPDATE
                    if (produtos.Alterar() == true)
                    {
                        MessageBox.Show("Produto atualizado com sucesso!");
                    }
                }
                else
                {
                    //INSERT
                    if (produtos.Incluir() == true)
                    {
                        MessageBox.Show("Produto cadastrado com sucesso!");
                    }
                }
                produtos = null;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                this.DialogResult = DialogResult.Abort;
            }

        }
    }
}

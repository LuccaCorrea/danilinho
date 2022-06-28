using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginForm;

namespace WindowsFormsApp3.Classes.Produtos
{
    public partial class GridProdutos : Form
    {
        public GridProdutos()
        {
            InitializeComponent();
        }


        private void GridProdutos_Load(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            try
            {
                Produtos alunos = new Produtos();
                dataGridView1.DataSource = alunos.Listar();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            FormProdutos fa = new FormProdutos();
            if (fa.ShowDialog() == DialogResult.OK) //se for ok sinal que foi clicado no gravar
            {
                CarregarGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja remover o(s) registro(s) selecionado(s)?", "Remover",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        int CI = int.Parse(dataGridView1.SelectedRows[i].Cells["CI"].Value.ToString());
                        Produtos aluno = new Produtos();
                        if (aluno.Deletar(CI))
                        {
                            dataGridView1.DataSource = aluno.Listar();
                        }
                    }
                    MessageBox.Show("Produto(s) deletados(s)!");

                }
                ///remover em lote
                ///dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i].Index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int CI = int.Parse(dataGridView1.SelectedRows[0].Cells["CI"].Value.ToString());
                MessageBox.Show("editando!");
                FormProdutos form = new FormProdutos(CI);
                if (form.ShowDialog() == DialogResult.OK) //se for ok sinal que foi clicado no gravar
                {
                    CarregarGrid();
                }
            }
            else
            {
                MessageBox.Show("Selecione apenas um registro!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int CI = int.Parse(dataGridView1.SelectedRows[0].Cells["CI"].Value.ToString());
                FormProdutos form = new FormProdutos(CI, true);
                form.Show();
            }
            else
            {
                MessageBox.Show("Selecione apenas um registro!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

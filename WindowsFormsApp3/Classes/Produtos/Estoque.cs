using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3.Classes.Produtos
{
    public class Estoque
    {


        public int ID { get; set; }
        public int CI_Produto { get; set; }
        public string End { get; set; }
        public string Qtd { get; set; }
        public string Fabricante { get; set; }
        public string ValorUnitario { get; set; }
        public string ContemCaixa { get; set; }

        public Estoque(int iD, int cI_Produto, string end, string qtd, string fabricante, string valorUnitario, string contemCaixa)
        {
            ID = iD;
            CI_Produto = cI_Produto;
            End = end;
            Qtd = qtd;
            Fabricante = fabricante;
            ValorUnitario = valorUnitario;
            ContemCaixa = contemCaixa;
        }

        public Estoque(int cI_Produto, string end, string qtd, string fabricante, string valorUnitario, string contemCaixa)
        {
            CI_Produto = cI_Produto;
            End = end;
            Qtd = qtd;
            Fabricante = fabricante;
            ValorUnitario = valorUnitario;
            ContemCaixa = contemCaixa;
        }

        public Estoque(string end, string qtd, string fabricante, string valorUnitario, string contemCaixa)
        {
            End = end;
            Qtd = qtd;
            Fabricante = fabricante;
            ValorUnitario = valorUnitario;
            ContemCaixa = contemCaixa;
        }

        public bool Incluir()
        {
            Database db = new Database();
            try
            {
                db.Conectar();

                long id = db.ExecutarComandoSQL("INSERT INTO tb_estoque VALUES(null, '" + this.CI_Produto +
                    "','" + this.End + "','" + this.Qtd + "','" + this.Fabricante +
                    "','" + this.ValorUnitario + "','" + this.ContemCaixa + "')", true);
                return true;
            }
            catch (Exception er)
            {
                throw new Exception("Erro ao cadastrar Endereço do Produto! - Erro: " + er.Message);
            }
            finally
            {
                db = null;
            }
        }

        public bool Alterar()
        {
            Database db = new Database();
            try
            {
                db.Conectar();

                db.ExecutarComandoSQL("UPDATE tb_estoque SET end = '" + this.End + "', " +
                    "           qtd = '" + this.Qtd + "', fabricante = '" + this.Fabricante + "', " +
                    "           valorUnitario = '" + this.ValorUnitario + "', contemCaixa = '" + this.ContemCaixa + "' " +
                    "           WHERE ci_produto = '" + this.CI_Produto + "'");
                return true;
            }
            catch (Exception er)
            {
                throw new Exception("Erro ao alterar estoque do Produto! - Erro: " + er.Message);
            }
            finally
            {
                db = null;
            }
        }
    }
}

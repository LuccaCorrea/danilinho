using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3.Classes.Produtos
{
    public class Produtos
    {
        public int CI { get; set; }
        public string SKU { get; set; }
        public string Descricao { get; set; }
        public string Lote { get; set; }
        public string TipoBebida { get; set; }
        public string TipoGarrafa { get; set; }
        public Estoque Estoque { get; set; }

        public Produtos(int cI, string sku, string descricao, string lote, string tipoBebida, string tipoGarrafa, Estoque endereco)
        {
            CI = cI;
            SKU = sku;
            Descricao = descricao;
            Lote = lote;
            TipoBebida = tipoBebida;
            TipoGarrafa = tipoGarrafa;
            Estoque = endereco;
        }

        public Produtos(string sku, string descricao, string lote, string tipoBebida, string tipoGarrafa, Estoque endereco)
        {
            SKU = sku;
            Descricao = descricao;
            Lote = lote;
            TipoBebida = tipoBebida;
            TipoGarrafa = tipoGarrafa;
            Estoque = endereco;
        }
        public Produtos() { }
        public bool Incluir()
        {
            Database db = new Database();
            try
            {
                db.Conectar();

                long ra = db.ExecutarComandoSQL("INSERT INTO tb_produtos VALUES(null, " +
                    "'" + this.SKU + "','" + this.Descricao + "','" + this.Lote + 
                    "','" + this.TipoBebida + "','" + this.TipoGarrafa + "')", true);

                Estoque.CI_Produto = int.Parse(ra.ToString());
                Estoque.Incluir();


                return true;
            }
            catch (Exception er)
            {
                throw new Exception("Erro ao cadastrar Produto! - Erro: " + er.Message);
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

                db.ExecutarComandoSQL("UPDATE tb_produtos SET sku = '" + this.SKU + "', descricao = '" + this.Descricao + "', lote = '" + this.Lote + "', " +
                                "tipoBebida ='" + this.TipoBebida + "', tipoGarrafa = '" + this.TipoGarrafa + "' WHERE CI = '" + this.CI + "'");

                Estoque.CI_Produto = this.CI;
                Estoque.Alterar();
                return true;
            }
            catch (Exception er)
            {
                throw new Exception("Erro ao alterar Produto! - Erro: " + er.Message);
            }
            finally
            {
                db = null;
            }
        }
        public DataTable Listar()
        {
            Database db = new Database();
            try
            {
                db.Conectar();
                return db.RetDataTable("SELECT CI, sku as 'SKU', descricao as 'Descricao', lote as 'Lote'," +
                    "tipoBebida as 'TipoBebida', tipoGarrafa as 'TipoGarrafa' FROM tb_produtos ORDER BY sku ASC");
            }
            catch (Exception er)
            {
                throw new Exception("Erro ao listar produtos! - Erro: " + er.Message);
            }
            finally
            {
                db = null;
            }
        }

        public bool Deletar(int CI)
        {
            Database db = new Database();
            try
            {
                db.Conectar();
                db.ExecutarComandoSQL("DELETE FROM tb_produtos WHERE CI = '" + CI + "'");
                return true;
            }
            catch (Exception er)
            {
                throw new Exception("Erro ao deletar Produto! - Erro: " + er.Message);
            }
            finally
            {
                db = null;
            }
        }

        public Produtos getProdutoPorCI(int CI)
        {
            Database db = new Database();
            try
            {
                db.Conectar();
                DataTable dados_produto = db.RetDataTable("SELECT * FROM tb_produtos WHERE CI = '" + CI + "'");
                DataTable dados_estoque = db.RetDataTable("SELECT * FROM tb_estoque WHERE ci_produto = '" + CI + "'");
                DataRow row = dados_produto.Rows[0];
                DataRow row_end = dados_estoque.Rows[0];
                
                Estoque endereco = new Estoque(int.Parse(row_end["id"].ToString()), int.Parse(row_end["ci_produto"].ToString()), row_end["end"].ToString(),
                    row_end["qtd"].ToString(), row_end["fabricante"].ToString(), row_end["valorUnitario"].ToString(), row_end["contemCaixa"].ToString());

                return new Produtos(int.Parse(row["CI"].ToString()), row["sku"].ToString(), row["descricao"].ToString(),
                    row["lote"].ToString(), row["tipoBebida"].ToString(), row["tipoGarrafa"].ToString(), endereco);
            }
            catch (Exception er)
            {
                throw new Exception("Erro ao listar Produto! - Erro: " + er.Message);
            }
            finally
            {
                db = null;
            }
        }
    }
}

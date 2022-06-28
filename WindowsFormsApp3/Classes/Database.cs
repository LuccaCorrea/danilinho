
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3.Classes
{
    public class Database
    {

        private MySqlConnection conn; //connection
        private DataTable data; // Gerencia Tabelas de Dados
        private MySqlDataAdapter da; // Adaptador | ex: EXCEL --> DataTable | MySql Table --> DataTable
        private MySqlDataReader dr; // Leitura de dados
        private MySqlCommandBuilder cb; //Construtor de Comandos SQL

        public static String server = "127.0.0.1";
        public static String user = "root";
        public static String password = "Lcs123@xD2";
        public static String database = "uscs";

        
        public void Conectar()
        {
            if (conn != null)
                conn.Close();

            string connStr = String.Format("Server={0};user id={1};password={2}; database={3};pooling=false", server, user, password, database);

            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //USADO PARA COMANDOS | INSERT - UPDATE - DELETE
        public long ExecutarComandoSQL(string comandoSql, bool ReturnLastInsertedId = false)
        {
            MySqlCommand comando = new MySqlCommand(comandoSql, conn);
            comando.ExecuteNonQuery();
            long id = comando.LastInsertedId;
            conn.Close();
            if (ReturnLastInsertedId == true)
            {
                return id;
            }
            else
            {
                return 0;
            }
        }

        //SELECTS
        public DataTable RetDataTable(string sql)
        {
            data = new DataTable();
            da = new MySqlDataAdapter(sql, conn);
            //cb = new MySqlCommandBuilder(da);
            da.Fill(data);
            return data;
        }

        public MySqlDataReader RetDataReader(string sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, conn);
            MySqlDataReader dr = comando.ExecuteReader();
            return dr;
        }
    }
}

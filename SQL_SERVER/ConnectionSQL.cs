using System;
using System.Data;
using System.Data.SqlClient;

namespace SQL_SERVER
{
    public class ConnectionSQL
    {
        public SqlConnection Connection_SQL_Server() {
            try
            {
                var datasource = "LEONARDO";//instancia do servidor
                var database = "BILTIFUL";//Base de dados
                var username = "sa"; //Usuario da conexao
                var password = "Terror912@"; //senha

                string connString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Persist Security Info=True;User Id=" + username + ";Password=" + password;

                return new SqlConnection(connString);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Conexao interrompida: " + ex.Message);
            }
            return null;
        }
    }
}

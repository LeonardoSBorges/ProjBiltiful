using CadastrosBasicos;
using ProducaoCosmeticos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ConexaoDB
{
    public class Conexao
    {
        public string DataSource { get; }
        public string DataBase { get; }
        public string UserName { get; }
        public string Password { get; }
        public string ConnString { get; }

        public Conexao()
        {
            DataSource = @"LOCALHOST";
            DataBase = "BILTIFUL";
            UserName = "sa";
            Password = "cpql1284";
            ConnString = @"Data Source=" + DataSource + ";Initial Catalog=" + DataBase + ";Persist Security Info=True;User ID=" + UserName + ";Password =" + Password;
        }

        public void GravarProducao(Producao producao)
        {
            SqlConnection connection = new(ConnString);
            string id = producao.Id;
            string dataProducao = producao.DataProducao;
            string produto = producao.Produto;
            float quantidade = producao.Quantidade;

            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO PRODUCAO VALUES ('{id}', CONVERT(DATE, '{dataProducao}', 111), '{produto}', '{quantidade}');";
                    connection.Open();
                    SqlCommand cmnd = new(sql, connection);
                    cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex -> " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}

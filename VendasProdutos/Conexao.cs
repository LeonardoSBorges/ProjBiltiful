using CadastrosBasicos;
using System;
using System.Data;
using System.Data.SqlClient;
using VendasProdutos;

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

        public void GravarVenda(Venda venda)
        {
            SqlConnection connection = new(ConnString);
            int id = venda.Id;
            string dataVenda = venda.DataVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string cpfCliente = venda.Cliente.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            decimal valorTotal = venda.ValorTotal;

            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO PRODUCAO VALUES ('{id}', CONVERT(DATE, '{dataVenda}', 111), '{cpfCliente}',  REPLACE('{valorTotal}', ',' , '.' ));";
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

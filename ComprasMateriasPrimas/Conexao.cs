using CadastrosBasicos;
using ComprasMateriasPrimas;
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

        public void GravarCompra(Compra compra)
        {
            SqlConnection connection = new(ConnString);
            int id = compra.Id;
            string dataCompra = compra.DataCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string fornecedor = compra.Fornecedor.Trim();
            float valorTotal = compra.ValorTotal;

            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO COMPRA VALUES ('{id}', CONVERT(DATE, '{dataCompra}', 111), {fornecedor},  REPLACE('{valorTotal}', ',' , '.' ));";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProducaoCosmeticos
{
    public class CadastrosBD2
    {

        public string DataSource { get; set; }

        public string DataBase { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConnString { get; set; }

        public CadastrosBD2()
        {

            DataSource = "DESKTOP-M6PNRRR";
            DataBase = "ProjetoBiltiful";
            Username = "sa";
            Password = "123456";
            ConnString = @"Data Source=" + DataSource + ";Initial Catalog="
                            + DataBase + ";Persist Security Info=True;User ID=" + Username + ";Password=" + Password;

        }



        //Producao:


        public void RegistraProducaoBD(string produto, float quantidade)
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                using (connection)
                {

                    InsereProducao(connection, produto, quantidade);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nFIM\n\nPressone qualquer tecla para finalizar");
            Console.ReadLine();

        }


        public static void InsereProducao(SqlConnection connection, string produto, float quantidade)
        {

            connection.Open();

            String sql = "INSERT INTO Producao (Produto, Quantidade) " +
                            "VALUES(@Produto, @Quantidade)";

            using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
            {
                sql_cmnd.Parameters.AddWithValue("@Produto", SqlDbType.NVarChar).Value = produto;
                sql_cmnd.Parameters.AddWithValue("@Quantidade", SqlDbType.Decimal).Value = quantidade;
                sql_cmnd.ExecuteNonQuery();
            }

            connection.Close();

        }


        public Producao BuscaProducao(string id)
        {

            Producao producao = null;
            string cod_produto;
            string dProducao;
            float quantidade;


            try
            {


                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"SELECT ID, Data_Producao, Produto, Quantidade FROM Producao WHERE ID = '{id}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            id = reader.GetValue(0).ToString();
                            dProducao = reader.GetValue(1).ToString();
                            cod_produto = reader.GetString(2);
                            quantidade = float.Parse(reader.GetValue(3).ToString());


                            producao = new(id, dProducao, cod_produto, quantidade);

                            connection.Close();

                            return producao;

                        }
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return producao;

        }


        public bool VerificaTabelaProducao()
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Close();
                connection.Open();

                String sql = $"SELECT ID FROM Producao";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Console.WriteLine("{0}", reader.GetValue(0));

                            return true;

                        }
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;

        }


        public List<Producao> ListaProducao()
        {

            List<Producao> lista_producoes = new List<Producao>();
            Producao producao;
            string cod_produto, id;
            string dProducao;
            float quantidade;
            ;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();


                String sql = $"SELECT ID, Data_Producao, Produto, Quantidade FROM Producao";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            id = reader.GetValue(0).ToString();
                            dProducao = reader.GetValue(1).ToString();
                            cod_produto = reader.GetString(2);
                            quantidade = float.Parse(reader.GetValue(3).ToString());


                            producao = new(id, dProducao, cod_produto, quantidade);

                            lista_producoes.Add(producao);

                        }
                    }
                    connection.Close();
                    return lista_producoes;

                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;

        }



        //ItemProducao:


        public void RegistraItemProducaoBD(string id, string codMPrima, float quantidadeMPrima)
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                using (connection)
                {

                    InsereItemProducao(connection, id, codMPrima, quantidadeMPrima);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nFIM\n\nPressone qualquer tecla para finalizar");
            Console.ReadLine();

        }


        public static void InsereItemProducao(SqlConnection connection, string id, string cod_MPrima, float quantidadeMPrima)
        {

            connection.Open();

            String sql = "INSERT INTO Item_Producao (ID, Id_Materia_Prima, Quantidade_Materia_Prima)" +
                            "VALUES(@ID,@Id_Materia_Prima, @Quantidade_Materia_Prima)";

            using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
            {
                sql_cmnd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = id;
                sql_cmnd.Parameters.AddWithValue("@Id_Materia_Prima", SqlDbType.NVarChar).Value = cod_MPrima;
                sql_cmnd.Parameters.AddWithValue("@Quantidade_Materia_Prima", SqlDbType.Decimal).Value = quantidadeMPrima;
                sql_cmnd.ExecuteNonQuery();
            }

            connection.Close();

        }


        public List<ItemProducao> ListaItemProducao()
        {

            List<ItemProducao> lista_itens_producao = new List<ItemProducao>();
            ItemProducao item_producao;
            string cod_mPrima, id;
            string dProducao;
            float quantidade_mPrima;
            ;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();


                String sql = $"SELECT  ID, Data_Producao, Id_Materia_Prima, Quantidade_Materia_Prima FROM Item_Producao";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            id = reader.GetValue(0).ToString();
                            dProducao = reader.GetValue(1).ToString();
                            cod_mPrima = reader.GetString(2);
                            quantidade_mPrima = float.Parse(reader.GetValue(3).ToString());


                            item_producao = new(id, dProducao, cod_mPrima, quantidade_mPrima);

                            lista_itens_producao.Add(item_producao);

                        }
                    }
                    connection.Close();
                    return lista_itens_producao;

                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;

        }


    }
}

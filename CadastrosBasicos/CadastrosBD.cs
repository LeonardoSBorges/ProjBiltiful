using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CadastrosBasicos
{
    public class CadastrosBD
    {

        public string DataSource { get; set; }

        public string DataBase { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConnString { get; set; }

        public CadastrosBD()
        {

            DataSource = "DESKTOP-M6PNRRR";
            DataBase = "ProjetoBiltiful";
            Username = "sa";
            Password = "123456";
            ConnString = @"Data Source=" + DataSource + ";Initial Catalog="
                            + DataBase + ";Persist Security Info=True;User ID=" + Username + ";Password=" + Password;

        }


        //Cliente:


        public void RegistraClienteBD(string cpf, string nome, DateTime dataNascimento, char sexo)
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                using (connection)
                {

                    InsereCliente(connection, cpf, nome, dataNascimento, sexo);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nFIM\n\nPressone qualquer tecla para finalizar");
            Console.ReadLine();

        }


        public static void InsereCliente(SqlConnection connection, string cpf, string nome, DateTime dataNascimento, char sexo)
        {

            connection.Open();

            String sql = "INSERT INTO Cliente(CPF, Nome, DataNasc, Sexo) " +
                            "VALUES(@CPF, @Nome, @DataNasc, @Sexo)";

            using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
            {

                sql_cmnd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;
                sql_cmnd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = nome;
                sql_cmnd.Parameters.AddWithValue("@DataNasc", SqlDbType.Date).Value = dataNascimento;
                sql_cmnd.Parameters.AddWithValue("@Sexo", SqlDbType.Char).Value = sexo;
                sql_cmnd.ExecuteNonQuery();
            }

            connection.Close();
        }


        public void BloqueiaCliente(string cpf)
        {

            bool risco = true;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"UPDATE Cliente SET Risco = @Risco WHERE CPF = '{cpf}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    sql_cmnd.Parameters.AddWithValue("@Risco", SqlDbType.Bit).Value = risco;
                    sql_cmnd.ExecuteNonQuery();

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public void DesbloqueiaCliente(string cpf)
        {

            bool risco = false;

            try
            {


                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"UPDATE Cliente SET Risco = @Risco WHERE CPF = '{cpf}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    sql_cmnd.Parameters.AddWithValue("@Risco", SqlDbType.Bit).Value = risco;
                    sql_cmnd.ExecuteNonQuery();

                }
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public bool VerificaCpfBloqueado(string cpf)
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"SELECT Risco FROM Cliente WHERE CPF = '{cpf}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Console.WriteLine("{0}", reader.GetBoolean(0));
                            connection.Close();
                            if (reader.GetBoolean(0))
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


        public Cliente BuscaCliente(string cpf)
        {

            Cliente cliente = null;
            string nome;
            char sexo, situacao;
            DateTime dNasc, uCompra, dCadastro;
            bool risco;


            try
            {


                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"SELECT CPF, Nome, DataNasc, Sexo, Ultima_Compra, Data_Cadastro, Situacao, Risco FROM Cliente WHERE CPF = '{cpf}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        { 

                            cpf = reader.GetString(0);
                            nome = reader.GetString(1);
                            dNasc = reader.GetDateTime(2);
                            sexo = char.Parse(reader.GetValue(3).ToString());
                            uCompra = reader.GetDateTime(4);
                            dCadastro = reader.GetDateTime(5);
                            situacao = char.Parse(reader.GetValue(6).ToString());
                            risco = reader.GetBoolean(7);


                            cliente = new(cpf, nome, dNasc, sexo, uCompra, dCadastro, situacao, risco);

                            connection.Close();

                            return cliente;

                        }
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return cliente;

        }


        public void EditaCliente(Cliente cliente_att)
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"UPDATE Cliente SET Nome = @Nome, DataNasc = @DataNasc, Situacao = @Situacao WHERE CPF = '{cliente_att.CPF}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    sql_cmnd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = cliente_att.Nome;
                    sql_cmnd.Parameters.AddWithValue("@DataNasc", SqlDbType.Date).Value = cliente_att.DataNascimento;
                    sql_cmnd.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = cliente_att.Situacao;
                    sql_cmnd.ExecuteNonQuery();

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public bool VerificaTabelaCliente()
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Close();
                connection.Open();

                String sql = $"SELECT CPF FROM Cliente";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Console.WriteLine("{0}", reader.GetString(0));

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


        public List<Cliente> ListaClientes()
        {

            List<Cliente> lista_clientes = new List<Cliente>();
            Cliente cliente;
            string cpf, nome;
            char sexo, situacao;
            DateTime dNasc, uCompra, dCadastro;
            bool risco;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"SELECT CPF, Nome, DataNasc, Sexo, Ultima_Compra, Data_Cadastro, Situacao, Risco FROM Cliente";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            cpf = reader.GetString(0);
                            nome = reader.GetString(1);
                            dNasc = reader.GetDateTime(2);
                            sexo = char.Parse(reader.GetValue(3).ToString());
                            uCompra = reader.GetDateTime(4);
                            dCadastro = reader.GetDateTime(5);
                            situacao = char.Parse(reader.GetValue(6).ToString());
                            risco = reader.GetBoolean(7);


                            cliente = new(cpf, nome, dNasc, sexo, uCompra, dCadastro, situacao, risco);

                            lista_clientes.Add(cliente);


                        }

                    }
                    connection.Close();
                    return lista_clientes;

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }



        //Fornecedor:


        public void RegistraFornecedorBD(string cnpj, string razao_social, DateTime dataAbertura)
        {

            try
            {
                SqlConnection connection = new SqlConnection(ConnString);

                using (connection)
                {

                    InsereFornecedor(connection, cnpj, razao_social, dataAbertura);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nFIM\n\nPressone qualquer tecla para finalizar");
            Console.ReadLine();

        }


        public static void InsereFornecedor(SqlConnection connection, string cnpj, string razao_social, DateTime dataAbertura)
        {

            connection.Open();

            String sql = "INSERT INTO Fornecedor (CNPJ, Razao_Social, Data_Abertura) " +
                            "VALUES(@CNPJ, @Razao_Social, @Data_Abertura)";

            using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
            {

                sql_cmnd.Parameters.AddWithValue("@CNPJ", SqlDbType.NVarChar).Value = cnpj;
                sql_cmnd.Parameters.AddWithValue("@Razao_Social", SqlDbType.NVarChar).Value = razao_social;
                sql_cmnd.Parameters.AddWithValue("@Data_Abertura", SqlDbType.Date).Value = dataAbertura;
                sql_cmnd.ExecuteNonQuery();
            }

            connection.Close();
        }


        public void BloqueiaFornecedor(string cnpj)
        {

            bool bloqueado = true;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"UPDATE Fornecedor SET Bloqueado = @Bloqueado WHERE CNPJ = '{cnpj}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    sql_cmnd.Parameters.AddWithValue("@Bloqueado", SqlDbType.Bit).Value = bloqueado;
                    sql_cmnd.ExecuteNonQuery();

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public void DesbloqueiaFornecedor(string cnpj)
        {

            bool bloqueado = false;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"UPDATE Fornecedor SET Bloqueado = @Bloqueado WHERE CNPJ = '{cnpj}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    sql_cmnd.Parameters.AddWithValue("@Bloqueado", SqlDbType.Bit).Value = bloqueado;
                    sql_cmnd.ExecuteNonQuery();

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public bool VerificaCnpjBloqueado(string cnpj)
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"SELECT Bloqueado FROM Fornecedor WHERE CNPJ = '{cnpj}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Console.WriteLine("{0}", reader.GetBoolean(0));
                            connection.Close();
                            if (reader.GetBoolean(0))
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


        public Fornecedor BuscaFornecedor(string cnpj)
        {

            Fornecedor fornecedor = null;
            string razao_social;
            char situacao;
            DateTime dAbertura, uCompra, dCadastro;
            bool bloqueado;


            try
            {


                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"SELECT CNPJ, Razao_Social, Data_Abertura, Ultima_Compra, Data_Cadastro, Situacao, Bloqueado FROM Fornecedor WHERE CNPJ = '{cnpj}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            cnpj = reader.GetString(0);
                            razao_social = reader.GetString(1);
                            dAbertura = reader.GetDateTime(2);
                            uCompra = reader.GetDateTime(3);
                            dCadastro = reader.GetDateTime(4);
                            situacao = char.Parse(reader.GetValue(5).ToString());
                            bloqueado = reader.GetBoolean(6);


                            fornecedor = new(cnpj, razao_social, dAbertura, uCompra, dCadastro, situacao, bloqueado);

                            connection.Close();

                            return fornecedor;

                        }
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return fornecedor;

        }


        public void EditaFornecedor(Fornecedor fornecedor_att)
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"UPDATE Fornecedor SET Razao_Social = @Razao_Social, Situacao = @Situacao WHERE CNPJ = '{fornecedor_att.CNPJ}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    sql_cmnd.Parameters.AddWithValue("@Razao_Social", SqlDbType.NVarChar).Value = fornecedor_att.RazaoSocial;
                    sql_cmnd.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = fornecedor_att.Situacao;
                    sql_cmnd.ExecuteNonQuery();

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public bool VerificaTabelaFornecedor()
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Close();
                connection.Open();

                String sql = $"SELECT CNPJ FROM Fornecedor";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Console.WriteLine("{0}", reader.GetString(0));

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


        public List<Fornecedor> ListaFornecedores()
        {

            List<Fornecedor> lista_fornecedores = new List<Fornecedor>();
            Fornecedor fornecedor;
            string cnpj, razao_social;
            char situacao;
            DateTime dAbertura, uCompra, dCadastro;
            bool bloqueado;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();


                String sql = $"SELECT CNPJ, Razao_Social, Data_Abertura, Ultima_Compra, Data_Cadastro, Situacao, Bloqueado FROM Fornecedor";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            cnpj = reader.GetString(0);
                            razao_social = reader.GetString(1);
                            dAbertura = reader.GetDateTime(2);
                            uCompra = reader.GetDateTime(3);
                            dCadastro = reader.GetDateTime(4);
                            situacao = char.Parse(reader.GetValue(5).ToString());
                            bloqueado = reader.GetBoolean(6);


                            fornecedor = new(cnpj, razao_social, dAbertura, uCompra, dCadastro, situacao, bloqueado);

                            lista_fornecedores.Add(fornecedor);



                        }
                    }
                    connection.Close();
                    return lista_fornecedores;

                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;

        }



        //Materia Prima:


        public void RegistraMPrimaBD(string ID, string nome)
        {


            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                using (connection)
                {

                    InsereMPrima(connection, ID, nome);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nFIM\n\nPressone qualquer tecla para finalizar");
            Console.ReadLine();


        }


        public static void InsereMPrima(SqlConnection connection, string ID, string nome)
        {

            connection.Open();

            String sql = "INSERT INTO Materia_Prima (ID, Nome) " +
                            "VALUES(@ID, @Nome)";

            using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
            {

                sql_cmnd.Parameters.AddWithValue("@ID", SqlDbType.NVarChar).Value = ID;
                sql_cmnd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = nome;
                sql_cmnd.ExecuteNonQuery();
            }

            connection.Close();
        }


        public MPrima BuscaMateriaPrima(string cod)
        {

            MPrima materia_prima = null;
            string nome;
            char situacao;
            DateTime uCompra, dCadastro;


            try
            {


                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"SELECT ID, Nome, Ultima_Compra, Data_Cadastro, Situacao FROM Materia_Prima WHERE ID = '{cod}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            cod = reader.GetString(0);
                            nome = reader.GetString(1);
                            uCompra = reader.GetDateTime(2);
                            dCadastro = reader.GetDateTime(3);
                            situacao = char.Parse(reader.GetValue(4).ToString());


                            materia_prima = new(cod, nome, uCompra, dCadastro, situacao);

                            connection.Close();

                            return materia_prima;

                        }
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return materia_prima;

        }


        public void AtualizaSituacaoMPrima(string cod, char situacao)
        {

            MPrima encontrada = BuscaMateriaPrima(cod);
            if (encontrada == null)
            {
                Console.WriteLine("\n A materia-prima nao existe.");
                Console.WriteLine("\n Pressione ENTER para voltar");
                Console.ReadKey();
            }
            else
            {

                try
                {

                    SqlConnection connection = new SqlConnection(ConnString);

                    connection.Open();

                    String sql = $"UPDATE Materia_Prima SET Situacao = @Situacao WHERE ID = '{cod}'";

                    using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                    {

                        sql_cmnd.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = situacao;
                        sql_cmnd.ExecuteNonQuery();

                    }

                    connection.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }


        public bool VerificaTabelaMPrima()
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Close();
                connection.Open();

                String sql = $"SELECT ID FROM Materia_Prima";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Console.WriteLine("{0}", reader.GetString(0));
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


        public List<MPrima> ListaMPrimas()
        {

            List<MPrima> lista_mprimas = new List<MPrima>();
            MPrima mPrima;
            string cod, nome;
            char situacao;
            DateTime uCompra, dCadastro;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();


                String sql = $"SELECT ID, Nome, Ultima_Compra, Data_Cadastro, Situacao FROM Materia_Prima";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            cod = reader.GetString(0);
                            nome = reader.GetString(1);
                            uCompra = reader.GetDateTime(2);
                            dCadastro = reader.GetDateTime(3);
                            situacao = char.Parse(reader.GetValue(4).ToString());


                            mPrima = new(cod, nome, uCompra, dCadastro, situacao);

                            lista_mprimas.Add(mPrima);



                        }
                    }
                    connection.Close();
                    return lista_mprimas;

                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;

        }



        //Produto:


        public void RegistraProdutoBD(string codigo_barras, string nome, decimal valor_venda)
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                using (connection)
                {

                    InsereProduto(connection, codigo_barras, nome, valor_venda);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nFIM\n\nPressone qualquer tecla para finalizar");
            Console.ReadLine();

        }


        public static void InsereProduto(SqlConnection connection, string codigo_barras, string nome, decimal valor_venda)
        {

            connection.Open();

            String sql = "INSERT INTO Produto(Codigo_Barras, Nome, Valor_Venda) " +
                "VALUES(@Codigo_Barras, @Nome, @Valor_Venda)";

            using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
            {

                sql_cmnd.Parameters.AddWithValue("@Codigo_Barras", SqlDbType.NVarChar).Value = codigo_barras;
                sql_cmnd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = nome;
                sql_cmnd.Parameters.AddWithValue("@Valor_Venda", SqlDbType.Decimal).Value = valor_venda;
                sql_cmnd.ExecuteNonQuery();
            }

            connection.Close();

        }


        public Produto BuscaProduto(string cod_barras)
        {

            Produto produto = null;
            string nome;
            char situacao;
            DateTime uVenda, dCadastro;
            decimal valor_venda;


            try
            {


                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();

                String sql = $"SELECT Codigo_Barras, Nome, Valor_Venda, Ultima_Venda, Data_Cadastro, Situacao FROM Produto WHERE Codigo_Barras = '{cod_barras}'";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            cod_barras = reader.GetString(0);
                            nome = reader.GetString(1);
                            valor_venda = reader.GetDecimal(2);
                            uVenda = reader.GetDateTime(3);
                            dCadastro = reader.GetDateTime(4);
                            situacao = char.Parse(reader.GetValue(5).ToString());


                            produto = new(cod_barras, nome, valor_venda, uVenda, dCadastro, situacao);

                            connection.Close();

                            return produto;

                        }
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return produto;

        }


        public void AtualizaSituacaoProduto(string cod_barras, char situacao)
        {

            Produto encontrado = BuscaProduto(cod_barras);
            if (encontrado == null)
            {
                Console.WriteLine("\n O produto nao existe.");
                Console.WriteLine("\n Pressione ENTER para voltar");
                Console.ReadKey();
            }
            else
            {

                try
                {

                    SqlConnection connection = new SqlConnection(ConnString);

                    connection.Open();

                    String sql = $"UPDATE Produto SET Situacao = @Situacao WHERE Codigo_Barras = '{cod_barras}'";

                    using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                    {

                        sql_cmnd.Parameters.AddWithValue("@Situacao", SqlDbType.Char).Value = situacao;
                        sql_cmnd.ExecuteNonQuery();

                    }

                    connection.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }


        public bool VerificaTabelaProduto()
        {

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Close();
                connection.Open();

                String sql = $"SELECT Codigo_Barras FROM Produto";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Console.WriteLine("{0}", reader.GetString(0));
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


        public List<Produto> ListaProduto()
        {

            List<Produto> lista_produtos = new List<Produto>();
            Produto produto;
            string cod_barras, nome;
            char situacao;
            DateTime uVenda, dCadastro;
            decimal valor_venda;

            try
            {

                SqlConnection connection = new SqlConnection(ConnString);

                connection.Open();


                String sql = $"SELECT Codigo_Barras, Nome, Valor_Venda, Ultima_Venda, Data_Cadastro, Situacao FROM Produto";

                using (SqlCommand sql_cmnd = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = sql_cmnd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            cod_barras = reader.GetString(0);
                            nome = reader.GetString(1);
                            valor_venda = reader.GetDecimal(2);
                            uVenda = reader.GetDateTime(3);
                            dCadastro = reader.GetDateTime(4);
                            situacao = char.Parse(reader.GetValue(5).ToString());


                            produto = new(cod_barras, nome, valor_venda, uVenda, dCadastro, situacao);

                            lista_produtos.Add(produto);



                        }
                    }
                    connection.Close();
                    return lista_produtos;

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

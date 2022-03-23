using CadastrosBasicos;
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


        public void GravarCliente(Cliente cliente)
        {
            SqlConnection connection = new(ConnString);
            string cpf = cliente.CPF.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            string nome = cliente.Nome.Trim();
            string dataNasc = cliente.DataNascimento.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char sexo = char.Parse(cliente.Sexo.ToString().ToUpper());
            string ultimaCompra = cliente.UltimaVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = cliente.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(cliente.Situacao.ToString().ToUpper());
            //int risco = 0;
            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO CLIENTE VALUES ('{cpf}', '{nome}', CONVERT(DATE, '{dataNasc}', 111), '{sexo}', CONVERT(DATE, '{ultimaCompra}', 111), CONVERT(DATE, '{dataCadastro}', 111), '{situacao}');";
                    connection.Open();
                    SqlCommand cmnd = new(sql, connection);
                    Console.WriteLine(cmnd.ToString());
                    cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX -> " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
        }

        public void EditarCliente(Cliente clienteAtualizado)
        {
            SqlConnection connection = new(ConnString);
            string cpf = clienteAtualizado.CPF.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            string nome = clienteAtualizado.Nome.Trim();
            string dataNasc = clienteAtualizado.DataNascimento.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char sexo = char.Parse(clienteAtualizado.Sexo.ToString().ToUpper());
            string ultimaCompra = clienteAtualizado.UltimaVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = clienteAtualizado.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(clienteAtualizado.Situacao.ToString().ToUpper());
            //int risco = 0;
            try
            {
                using (connection)
                {
                    string sql = $"UPDATE CLIENTE SET CPF = '{cpf}', NOME = '{nome}', DATA_NASCIMENTO = CONVERT(DATE, '{dataNasc}', 111), SEXO = '{sexo}', ULTIMA_COMPRA = CONVERT(DATE, '{ultimaCompra}', 111), DATA_CADASTRO = CONVERT(DATE, '{dataCadastro}', 111), SITUACAO = '{situacao}' WHERE CPF = '{cpf}'";
                    Console.WriteLine(sql);
                    connection.Open();
                    SqlCommand cmnd = new(sql, connection);
                    Console.WriteLine(cmnd.ToString());
                    cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX -> " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
        }

        public void ProcurarCliente(string cpf)
        {
            SqlConnection connection = new(ConnString);
            connection.Open();

            string sql = $"SELECT CPF, NOME, CONVERT(DATE, DATA_NASCIMENTO, 111), SEXO, CONVERT(DATE, ULTIMA_COMPRA, 111), CONVERT(DATE, DATA_CADASTRO, 111), SITUACAO FROM CLIENTE WHERE CPF = '{cpf}';";
            using (SqlCommand cmnd = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = cmnd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0), reader.GetString(1), reader.GetSqlDateTime(2), reader.GetChar(3), reader.GetSqlDateTime(4), reader.GetSqlDateTime(5),reader.GetChar(6));
                    }
                }
            }
            connection.Close();
        }

        public void GravarFornecedor(Fornecedor fornecedor)
        {
            SqlConnection connection = new(ConnString);
            string cnpj = fornecedor.CNPJ.Insert(2, ".").Insert(6, ".").Insert(9, "/").Insert(15, "-");
            string razaoSocial = fornecedor.RazaoSocial.Trim();
            string dataAbertura = fornecedor.DataAbertura.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string ultimaCompra = fornecedor.UltimaCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = fornecedor.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(fornecedor.Situacao.ToString().ToUpper());
            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO FORNECEDOR VALUES ('{cnpj}', '{razaoSocial}', CONVERT(DATE, '{dataAbertura}', 111), CONVERT(DATE, '{ultimaCompra}', 111), CONVERT(DATE, '{dataCadastro}', 111), '{situacao}');";
                    connection.Open();
                    SqlCommand cmnd = new(sql, connection);
                    cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX -> " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void EditarFornecedor(Fornecedor fornecedor)
        {
            SqlConnection connection = new(ConnString);
            string cnpj = fornecedor.CNPJ.Insert(2, ".").Insert(6, ".").Insert(9, "/").Insert(15, "-");
            string razaoSocial = fornecedor.RazaoSocial.Trim();
            string dataAbertura = fornecedor.DataAbertura.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string ultimaCompra = fornecedor.UltimaCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = fornecedor.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(fornecedor.Situacao.ToString().ToUpper());
            try
            {
                using (connection)
                {
                    string sql = $"UPDATE FORNECEDOR SET CNPJ = '{cnpj}', RAZAO_SOCIAL = '{razaoSocial}', DATA_ABERTURA = CONVERT(DATE, '{dataAbertura}', 111), ULTIMA_COMPRA = CONVERT(DATE, '{ultimaCompra}', 111), DATA_CADASTRO = CONVERT(DATE, '{dataCadastro}', 111), SITUACAO = '{situacao}' WHERE CNPJ = '{cnpj}'";
                    Console.WriteLine(sql);
                    connection.Open();
                    SqlCommand cmnd = new(sql, connection);
                    Console.WriteLine(cmnd.ToString());
                    cmnd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EX -> " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
        }

        public void ProcurarFornecedor(string cnpj)
        {
            SqlConnection connection = new(ConnString);

            connection.Open();

            string sql = $"SELECT CNPJ, RAZAO_SOCIAL, CONVERT(DATE, DATA_ABERTURA, 111), CONVERT(DATE, ULTIMA_COMPRA, 111), CONVERT(DATE, DATA_CADASTRO, 111), SITUACAO FROM FORNECEDOR WHERE CNPJ = '{cnpj}';";
            using (SqlCommand cmnd = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = cmnd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0), reader.GetString(1), reader.GetSqlDateTime(2), reader.GetSqlDateTime(3), reader.GetSqlDateTime(4), reader.GetChar(5));
                    }
                }
            }
            connection.Close();
        }

        public void GravarNovoProduto(Produto produto)
        {
            SqlConnection connection = new(ConnString);
            string codigoBarras = produto.CodigoBarras;
            string nome = produto.Nome.Trim();
            decimal valorVenda = produto.ValorVenda;
            string ultimaVenda = produto.UltimaVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = produto.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(produto.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO PRODUTO VALUES ('{codigoBarras}', '{nome}',  REPLACE('{valorVenda}', ',' , '.' ), CONVERT(DATE, '{ultimaVenda}', 111), CONVERT(DATE, '{dataCadastro}', 111), '{situacao}');";
                    Console.WriteLine(sql);
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

        public void EditarProduto(Produto produto)
        {
            SqlConnection connection = new(ConnString);
            string codigoBarras = produto.CodigoBarras;
            string nome = produto.Nome.Trim();
            decimal valorVenda = produto.ValorVenda;
            string ultimaVenda = produto.UltimaVenda.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = produto.DataCadastro.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            char situacao = char.Parse(produto.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"UPDATE PRODUTO SET CODIGO_BARRAS = '{codigoBarras}', NOME = '{nome}',  VALOR_VENDA = REPLACE('{valorVenda}', ',' , '.' ), ULTIMA_VENDA = CONVERT(DATE, '{ultimaVenda}', 111), DATA_CADASTRO = CONVERT(DATE, '{dataCadastro}', 111), SITUACAO = '{situacao}');";
                    Console.WriteLine(sql);
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

        public void ProcurarProduto(string codigo_barras)
        {

            SqlConnection connection = new(ConnString);

            connection.Open();

            string sql = $"SELECT CODIGO_BARRAS, NOME,  REPLACE(VALOR_VENDA, ',' , '.' ), CONVERT(DATE, ULTIMA_VENDA, 111), CONVERT(DATE, DATA_CADASTRO, 111), SITUACAO FROM PRODUTO WHERE CODIGO_BARRAS = '{codigo_barras}';";
            using (SqlCommand cmnd = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = cmnd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0), reader.GetString(1), reader.GetDecimal(2), reader.GetSqlDateTime(3), reader.GetSqlDateTime(4), reader.GetChar(5));
                    }
                }
            }
            connection.Close();

        }

        public void GravarMateriaPrimaBD(MPrima materiaPrima)
        {
            SqlConnection connection = new(ConnString);
            string id = materiaPrima.Id;
            string nome = materiaPrima.Nome.Trim();
            string ultimaCompra = materiaPrima.UltimaCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = materiaPrima.DataCadastro.Date.ToString("yyyy/MM/dd").Replace('/', '-');
            char situacao = char.Parse(materiaPrima.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"INSERT INTO MATERIA_PRIMA VALUES ('{id}', '{nome}', CONVERT(DATE, '{ultimaCompra}', 111), CONVERT(DATE, '{dataCadastro}', 111), '{situacao}');";
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

        public void EditarMateriaPrima(MPrima materiaPrima)
        {
            SqlConnection connection = new(ConnString);
            string id = materiaPrima.Id;
            string nome = materiaPrima.Nome.Trim();
            string ultimaCompra = materiaPrima.UltimaCompra.Date.ToString("yyyy/MM/dd").Replace("/", "-");
            string dataCadastro = materiaPrima.DataCadastro.Date.ToString("yyyy/MM/dd").Replace('/', '-');
            char situacao = char.Parse(materiaPrima.Situacao.ToString().ToUpper());

            try
            {
                using (connection)
                {
                    string sql = $"UPDATE MATERIA_PRIMA SET ID = '{id}', NOME = '{nome}', ULTIMA_COMPRA = CONVERT(DATE, '{ultimaCompra}', 111), DATA_CADASTRO = CONVERT(DATE, '{dataCadastro}', 111), SITUACAO = '{situacao}'";
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

        public void ProcurarMateriaPrima(string id)
        {
            SqlConnection connection = new(ConnString);

            connection.Open();

            string sql = $"SELECT ID, NOME, CONVERT(DATE, ULTIMA_COMPRA, 111), CONVERT(DATE, DATA_CADASTRO, 111), SITUACAO FROM MATERIA_PRIMA WHERE ID = '{id}';";

            using (SqlCommand cmnd = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = cmnd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0), reader.GetSqlDateTime(1), reader.GetSqlDateTime(2), reader.GetSqlDateTime(3), reader.GetChar(4));
                    }
                }
            }
            connection.Close();
        }
    }
}


using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CadastrosBasicos
{
    public class BDCadastro
    {
        private SqlConnection _connection = new ConnectionSQL().Connection_SQL_Server();

        public void PushNewRegister(string newRegister)
        {
            _connection.Open();
            using (_connection)
            {
                using (SqlCommand conn = new SqlCommand(newRegister, _connection))
                {
                    conn.ExecuteNonQuery();
                }
            }
            _connection.Close();
        }
        public string SearchData(string sqlCommand)
        {
            string getValue = "";
            _connection.Open();
            using (SqlCommand conn = new SqlCommand(sqlCommand, _connection))
            {
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getValue = $"{reader.GetValue(0)}, {reader.GetValue(1)}, {reader.GetValue(2)}, {reader.GetValue(3)}, {reader.GetValue(4)}, {reader.GetValue(5)}, {reader.GetValue(6)}";
                    }
                }
            }
            _connection.Close();
            return getValue;
        }
        public string SearchDataProduct(string codigo)
        {
            string getValue = "";
            _connection.Open();
            using (SqlCommand conn = new SqlCommand(codigo, _connection))
            {
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getValue = $"{reader.GetValue(0)}, {reader.GetValue(1)}, {reader.GetValue(2)}, {reader.GetValue(3)}, {reader.GetValue(4)}, {reader.GetValue(5)}, {reader.GetValue(6)}";
                    }
                }
            }
            _connection.Close();

            return getValue;
        }
        public Produto GetDataProduct(string codigo)
        {
            Produto produto = new Produto();
            _connection.Open();
            using (SqlCommand conn = new SqlCommand(codigo, _connection))
            {
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produto.CodigoBarras = reader.GetValue(0).ToString();
                        produto.Nome = reader.GetValue(1).ToString();
                        produto.ValorVenda = reader.GetDecimal(2);
                        produto.UltimaVenda = reader.GetDateTime(3);
                        produto.DataCadastro = reader.GetDateTime(4);
                        produto.Situacao = char.Parse(reader.GetValue(5).ToString());
                    }
                }
            }
            _connection.Close();
            return produto;
        }
        public List<Produto> ListProduto()
        {
            List<Produto> produtos = new List<Produto>();
            Produto produto = new Produto();

            using (SqlCommand conn = new SqlCommand("SELECT * FROM Cliente", _connection))
            {
                _connection.Open();
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produto.CodigoBarras = reader.GetValue(0).ToString();
                        produto.Nome = reader.GetValue(1).ToString();
                        produto.ValorVenda = reader.GetDecimal(2);
                        produto.UltimaVenda = reader.GetDateTime(3);
                        produto.DataCadastro = reader.GetDateTime(4);
                        produto.Situacao = char.Parse(reader.GetValue(5).ToString());
                        
                        produtos.Add(produto);
                    }
                }

                _connection.Close();
            }
            return produtos;
        }
        public string SearchDataFornecedor(string sqlCommand)
        {
            string getValue = "";
            _connection.Open();
            using (SqlCommand conn = new SqlCommand(sqlCommand, _connection))
            {
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getValue = $"{reader.GetValue(0)}, {reader.GetValue(1)}, {reader.GetValue(2)}, {reader.GetValue(3)}, {reader.GetValue(4)}, {reader.GetValue(5)}";
                    }
                }
            }
            _connection.Close();
            return getValue;
        }
        public List<Cliente> ListCliente()
        {
            List<Cliente> clientes = new List<Cliente>();
            string cpf = "";
            string nome = "";
            DateTime dNascimento;
            char sexo;
            DateTime uCompra;
            DateTime dCadastro;
            char situacao;

            using (SqlCommand conn = new SqlCommand("SELECT * FROM Cliente", _connection))
            {
                _connection.Open();
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cpf = reader.GetValue(0).ToString();
                        nome = reader.GetValue(1).ToString();
                        dNascimento = reader.GetDateTime(2);
                        sexo = char.Parse(reader.GetValue(3).ToString());
                        uCompra = reader.GetDateTime(4);
                        dCadastro = reader.GetDateTime(5);
                        situacao = char.Parse(reader.GetValue(6).ToString());
                        Cliente cliente = new Cliente(cpf, nome, dNascimento, sexo, uCompra, dCadastro, situacao);
                        clientes.Add(cliente);
                    }
                }

                _connection.Close();
            }
            return clientes;
        }
        public List<Fornecedor> ListFornecedor()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            string cnpj = "";
            string rSocial = "";
            DateTime dAbertura;
            DateTime uCompra;
            DateTime dCadastro;
            char situacao;
            _connection.Open();
            using (SqlCommand conn = new SqlCommand("SELECT * FROM Fornecedor", _connection))
            {
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cnpj = reader.GetValue(0).ToString();
                        rSocial = reader.GetValue(1).ToString();
                        dAbertura = reader.GetDateTime(2);
                        uCompra = reader.GetDateTime(3);
                        dCadastro = reader.GetDateTime(4);
                        situacao = char.Parse(reader.GetValue(5).ToString());
                        Fornecedor fornecedor = new Fornecedor(cnpj, rSocial, dAbertura, uCompra, dCadastro, situacao);
                        fornecedores.Add(fornecedor);
                    }
                }
                _connection.Close();
            }
            return fornecedores;
        }
        public string SearchBlocked(string value)
        {
            string getValue = "";
            _connection.Open();
            using (SqlCommand conn = new SqlCommand(value, _connection))
            {
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getValue = $"{reader.GetValue(0)}";
                    }
                }
            }
            _connection.Close();
            return getValue;
        }
        public string UltimoCodigoMateriaPrima()
        {
            _connection.Close();
            string getValue = "";
            _connection.Open();
            using (SqlCommand conn = new SqlCommand($"SELECT Top (1) [Codigo] FROM [BILTIFUL].[dbo].[MateriaPrima]", _connection))
            {
                using (SqlDataReader reader = conn.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        getValue = reader.GetValue(0).ToString();
                    }
                }
            }
            _connection.Close();
            return getValue;
        }

    }
}

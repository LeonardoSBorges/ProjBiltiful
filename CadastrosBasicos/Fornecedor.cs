using CadastrosBasicos.ManipulaArquivos;
using ConexaoDB;
using System;
using System.Collections.Generic;
using System.IO;

namespace CadastrosBasicos
{
    public class Fornecedor
    {
        public Write write = new Write();
        public Read read = new Read();
        public BDCadastro connection = new BDCadastro();
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        Conexao conexao = new Conexao();
        public Fornecedor()
        {

        }
        public Fornecedor(string cnpj)
        {
            CNPJ = cnpj;
        }
        public Fornecedor(string cnpj, string rSocial, DateTime dAbertura, char situacao)
        {
            CNPJ = cnpj;
            RazaoSocial = rSocial;
            DataAbertura = dAbertura;
            UltimaCompra = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public Fornecedor(string cnpj, string rSocial, DateTime dAbertura, DateTime uCompra, DateTime dCadastro, char situacao)
        {
            CNPJ = cnpj;
            RazaoSocial = rSocial;
            DataAbertura = dAbertura;
            UltimaCompra = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public void Navegar()
        {
            Console.WriteLine("============== Fornecedores ==============");
            List<Fornecedor> lista = connection.ListFornecedor();
            if (lista != null)
            {
                int opcao = 0, posicao = 0;
                bool flag = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine("============== Fornecedores ==============");

                    if (opcao == 0)
                    {
                        Console.WriteLine(lista[posicao].ToString());
                    }
                    else if (opcao == 1)
                    {
                        if (posicao != lista.Count - 1)
                            posicao++;

                        Console.WriteLine(lista[posicao].ToString());
                    }
                    else if (opcao == 2)
                    {
                        if (posicao != 0)
                            posicao--;

                        Console.WriteLine(lista[posicao].ToString());
                    }
                    else if (opcao == 3)
                    {
                        posicao = 0;
                        Console.WriteLine(lista[posicao].ToString());
                    }
                    else if (opcao == 4)
                    {
                        posicao = lista.Count - 1;
                        Console.WriteLine(lista[posicao].ToString());
                    }

                    Console.WriteLine(@"
1. Proximo 
2. Anterior
3. Primeiro
4. Ultimo
0. Voltar para menu anterior.
");
                    do
                    {
                        flag = int.TryParse(Console.ReadLine(), out opcao);
                    } while (flag != true);

                } while (opcao != 0);

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ainda nao tem nenhum fornecedor cadastrado");
                Console.WriteLine("Pressione enter para continuar");
                Console.ReadKey();
            }
        }
        public void Localizar()
        {
            Console.WriteLine("Insira o CNPJ para localizar: ");
            string cnpj = Console.ReadLine();
            string fornecedor = connection.SearchDataFornecedor($"SELECT * FROM Fornecedor WHERE CNPJ = '{cnpj}'");


            if (fornecedor.Length != 0)
            {
                Console.WriteLine(fornecedor);
            }
            else
                Console.WriteLine("Nenhum cadastrado foi encontrado!");
            Console.WriteLine("Pressione enter para voltar ao menu.");
            Console.ReadKey();
        }
        public void BloqueiaFornecedor()
        {
            Console.WriteLine("Insira o CNPJ para bloqueio: ");
            string cnpj = Console.ReadLine();
            string getFornecedor = connection.SearchDataFornecedor($"SELECT * FROM Fornecedor WHERE CNPJ = '{cnpj}'");
            string getFornecedorBloqueado = connection.SearchBlocked($"SELECT * FROM FornecedorBloqueado WHERE CNPJ_Fornecedor = '{cnpj}'");
            if (getFornecedorBloqueado.Length != 0)
            {
                bool flag = false;
                int opcao;
                Console.WriteLine("Fornecedor ja esta bloqueado");
                Console.WriteLine("Deseja desbloqueado ? [1 - Sim/ 2 - Nao]");

                do
                {
                    flag = int.TryParse(Console.ReadLine(), out opcao);
                } while (flag != true);

                if (opcao == 1)
                {
                    connection.PushNewRegister($"DELETE FROM FornecedorBloqueado WHERE CNPJ_Fornecedor= '{cnpj}'");
                    Console.WriteLine("Fornecedor desbloqueado");
                }
                else
                    Console.WriteLine("Insira uma opcao valida");
                
            }
            else
            {
                if (Validacoes.ValidarCnpj(cnpj))
                {
                    if (getFornecedor.Length != 0)
                    {
                        connection.PushNewRegister($"INSERT INTO FornecedorBloqueado(CNPJ_Fornecedor) VALUES ('{cnpj}')");
                        Console.WriteLine("CNPJ bloqueado!");
                    }
                }
                else
                {
                    Console.WriteLine("CNPJ incorreto!");
                }
            }
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }
        public string RetornaArquivo()
        {
            return CNPJ + RazaoSocial + DataAbertura.ToString("dd/MM/yyyy") + UltimaCompra.ToString("dd/MM/yyyy") + DataCadastro.ToString("dd/MM/yyyy") + Situacao;
        }

        public Fornecedor Editar()
        {

            Fornecedor fornecedor;
            Console.WriteLine("Somente algumas informacoes podem ser alterada como (Razao social/situacao), caso nao queira alterar alguma informacao pressione enter!");
            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine();
            
            fornecedor = read.ProcurarFornecedor(cnpj);
            if (fornecedor != null)
            {
                Console.WriteLine("Razao social: ");
                string nome = Console.ReadLine().Trim().PadLeft(50, ' ');
                Console.WriteLine("Situacao [A - Ativo/ I - inativo]: ");
                bool flagSituacao = char.TryParse(Console.ReadLine().ToString().ToUpper(), out char situacao);

                fornecedor.RazaoSocial = nome == "" ? fornecedor.RazaoSocial : nome;

                fornecedor.Situacao = flagSituacao == false ? fornecedor.Situacao : situacao;

                //write.EditarFornecedor(fornecedor);
                conexao.EditarFornecedor(fornecedor);
            }
            return fornecedor;
        }

        public void FornecedorBloqueado()
        {
            Console.WriteLine("Insira o CNPJ para pesquisa: ");
            string cnpj = Console.ReadLine();
            string cnpjBloqueado = connection.SearchBlocked($"SELECT * FROM FornecedorBloqueado WHERE CNPJ_Fornecedor = '{cnpj}'");
            
            if (cnpjBloqueado.Length != 0)
            {
                string procuraFornecedor = $"SELECT * FROM Fornecedor WHERE CNPJ = '{cnpj}'";
                Console.WriteLine(connection.SearchDataFornecedor(procuraFornecedor));
            }
            else
            {
                Console.WriteLine("Fornecedor bloqueado nao encontrado");
            }
            Console.WriteLine("Pressione enter para continuar...");
            Console.ReadKey();
        }
        public override string ToString()
        {
            return $"CNPJ: {CNPJ}\nRSocial: {RazaoSocial.Trim()}\nData de Abertura da empresa: {DataAbertura.ToString("dd/MM/yyyy")}\nUltima Compra: {UltimaCompra.ToString("dd/MM/yyyy")}\nData de Cadastro: {DataCadastro.ToString("dd/MM/yyyy")}\nSituacao: {Situacao}";
        }
    }
}

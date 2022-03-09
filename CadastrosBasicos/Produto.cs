using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CadastrosBasicos
{
    public class Produto
    {
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public override string ToString()
        {
            return CodigoBarras
                + Nome.PadLeft(20, ' ')
                + ValorVenda.ToString("000.#0").Replace(",", "")
                + UltimaVenda.ToString("dd/MM/yyyy").Replace("/", "")
                + DataCadastro.ToString("dd/MM/yyyy").Replace("/", "")
                + Situacao;
        }

        public Produto()
        {

        }

        public Produto(string cBarras, string nome, decimal vVenda, DateTime uVenda, DateTime dCadastro, char situacao)
        {
            CodigoBarras = cBarras;
            Nome = nome;
            ValorVenda = vVenda;
            UltimaVenda = uVenda;
            DataCadastro = dCadastro;
            Situacao = situacao;
        }

        public void Menu()
        {
            string escolha;

            do
            {
                Console.Clear();
                Console.WriteLine("\n=============== PRODUTO ===============");
                Console.WriteLine("1. Cadastrar Produto");
                Console.WriteLine("2. Localizar Produto");
                Console.WriteLine("3. Imprimir Produtos");
                Console.WriteLine("4. Alterar Situacao do Produto");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("0. Voltar ao menu anterior");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        break;
                    case "1":
                        Cadastrar();
                        break;
                    case "2":
                        Localizar();
                        break;
                    case "3":
                        ImprimirProdutos();
                        break;
                    case "4":
                        AlterarSituacao();
                        break;

                    default:
                        Console.WriteLine("\n Opção inválida.");
                        Console.WriteLine("\n Pressione ENTER para voltar ao menu.");
                        Console.ReadKey();
                        break;
                }

            } while (escolha != "0");
        }


        public void Cadastrar()
        {
            Produto produto = new Produto();

            char sit = 'A';
            string cod, nomeTemp, verificaProduto = null;
            decimal valorVenda = 0;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine("\n Cadastro de Produto\n");

                do
                {
                    Console.Clear();

                    Console.Write(" Cod. Barras: 789");
                    cod = Console.ReadLine();

                    if (string.IsNullOrEmpty(cod))
                    {
                        Console.WriteLine("\n Nenhum campo podera ser vazio.");
                        Console.WriteLine("\n Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                        continue;

                    }

                    cod = "789" + cod;

                    if (cod.Length != 13)
                    {
                        Console.WriteLine("\n Codigo inválido. Digite um código de 13 digitos, informando apenas os ultimos 10 numeros.");
                        Console.WriteLine("\n Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                        continue;
                    }

                    verificaProduto = Buscar(cod);

                    if (!string.IsNullOrEmpty(verificaProduto))
                    {
                        Console.WriteLine("\n Ja existe um produto cadastrado com esse codigo.");
                        Console.WriteLine("\n Pressione ENTER para voltar...");
                        Console.ReadKey();
                    }

                } while (cod.Length != 13 || !string.IsNullOrEmpty(verificaProduto));


                do
                {
                    Console.Write(" Nome: ");
                    nomeTemp = Console.ReadLine();

                    if (nomeTemp.Length > 20 || string.IsNullOrEmpty(nomeTemp))
                    {
                        Console.WriteLine("\n Nome invalido. Digite apenas 20 caracteres.");
                        Console.WriteLine("\n Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }

                } while (nomeTemp.Length > 20 || string.IsNullOrEmpty(nomeTemp));

                do
                {
                    Console.Write(" Valor da Venda: ");
                    valorVenda = Convert.ToDecimal(Console.ReadLine());

                    if ((valorVenda < 1) || (valorVenda > (decimal) 999.99))
                    {
                        Console.WriteLine("\n Valor invalido. Apenas valores maior que 0 e menor que 999,99.");
                        Console.WriteLine("\n Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }

                } while ((valorVenda < 1) || (valorVenda > (decimal) 999.99));


                do
                {
                    Console.Write(" Situacao (A / I): ");
                    sit = char.Parse(Console.ReadLine().ToUpper());

                    if ((sit != 'A') && (sit != 'I') || string.IsNullOrEmpty(sit.ToString()))
                    {
                        Console.WriteLine("\n Situacao invalida.");
                        Console.WriteLine("\n Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                        continue;
                    }

                } while ((sit != 'A') && (sit != 'I'));


                flag = false;

                produto.CodigoBarras = cod;
                produto.Nome = nomeTemp;
                produto.ValorVenda = valorVenda;
                produto.UltimaVenda = DateTime.Now.Date;
                produto.DataCadastro = DateTime.Now.Date;
                produto.Situacao = sit;

                GravarProduto(produto);

                Console.WriteLine("\n Cadastro do Produto concluido com sucesso!\n");
                Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                Console.ReadKey();

            } while (flag);
        }

        public void GravarProduto(Produto produto)
        {
            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoFinal = Path.Combine(caminhoFinal, "Cosmetico.dat");

            try
            {
                if (!File.Exists(arquivoFinal))
                {
                    using (StreamWriter sw = new StreamWriter(arquivoFinal))
                    {
                        sw.WriteLine(produto.ToString());
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(arquivoFinal, append: true))
                    {
                        sw.WriteLine(produto.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex -> " + ex.Message);
            }
        }

        public void Localizar()
        {
            string cod, produto;

            Console.Clear();
            Console.WriteLine("\n Localizar Produto");
            Console.Write("\n Digite o codigo do produto: ");
            cod = Console.ReadLine();

            produto = Buscar(cod);

            if (produto == null)
            {
                Console.WriteLine("\n O produto nao existe.");
                Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                Console.ReadKey();
            }
            else
            {
                string situacao = produto.Substring(54, 1);
                if (situacao == "A")
                    situacao = "Ativo";
                else if (situacao == "I")
                    situacao = "Inativo";

                Console.WriteLine("\n O produto foi encontrado.\n");
                Console.WriteLine($" Codigo: {produto.Substring(0, 13)}");
                Console.WriteLine($" Nome: {produto.Substring(13, 20)}");
                Console.WriteLine($" Valor da venda: {produto.Substring(33, 5).Insert(3, ",")}");
                Console.WriteLine($" Data ultima venda: {produto.Substring(38, 8).Insert(2, "/").Insert(5, "/")}");
                Console.WriteLine($" Data do cadastro: {produto.Substring(46, 8).Insert(2, "/").Insert(5, "/")}");
                Console.WriteLine($" Situacao: {situacao}");
                Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                Console.ReadKey();
            }
        }

        public void AlterarSituacao()
        {
            string cod, produto, situacao;
            bool flag = true;

            Console.Clear();
            Console.WriteLine("\n Alterar Produto");
            Console.Write("\n Digite o codigo do produto: ");
            cod = Console.ReadLine();

            produto = Buscar(cod);

            if (produto == null)
            {
                Console.WriteLine("\n O produto nao existe.");
                Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                Console.ReadKey();
            }
            else
            {
                situacao = produto.Substring(54, 1);
                if (situacao == "A")
                    situacao = "Ativo";
                else if (situacao == "I")
                    situacao = "Inativo";

                Console.WriteLine("\n O produto foi encontrado.\n");
                Console.WriteLine($" Codigo: {produto.Substring(0, 13)}");
                Console.WriteLine($" Nome: {produto.Substring(13, 20)}");
                Console.WriteLine($" Valor da venda: {produto.Substring(33, 5).Insert(3, ",")}");
                Console.WriteLine($" Data ultima venda: {produto.Substring(38, 8).Insert(2, "/").Insert(5, "/")}");
                Console.WriteLine($" Data do cadastro: {produto.Substring(46, 8).Insert(2, "/").Insert(5, "/")}");
                Console.WriteLine($" Situacao: {situacao}");

                do
                {
                    Console.Write("\n Qual a nova situacao do produto (A / I): ");
                    situacao = Console.ReadLine().ToUpper();

                    if ((situacao != "A") && (situacao != "I"))
                    {
                        Console.WriteLine("\n Situacao invalida.");
                        Console.WriteLine("\n Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }
                    else
                    {
                        flag = false;
                    }

                } while (flag);

                Atualizar(cod, null, situacao);
            }
        }

        public void ImprimirProdutos()
        {
            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoFinal = Path.Combine(caminhoFinal, "Cosmetico.dat");

            List<Produto> Produtos = new List<Produto>();

            if (File.Exists(arquivoFinal))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivoFinal))
                    {
                        string line = sr.ReadLine();
                        do
                        {
                            if (line.Substring(54, 1) != "I")
                            {
                                Produtos.Add(
                                    new Produto(
                                        line.Substring(0, 13),
                                        line.Substring(13, 20),
                                        Convert.ToDecimal(line.Substring(33, 5).Insert(3, ",")),
                                        Convert.ToDateTime(line.Substring(38, 8).Insert(2, "/").Insert(5, "/")).Date,
                                        Convert.ToDateTime(line.Substring(46, 8).Insert(2, "/").Insert(5, "/")).Date,
                                        Convert.ToChar(line.Substring(54, 1))
                                        )
                                    );
                            }
                            line = sr.ReadLine();

                        } while (line != null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ex ->" + ex.Message);
                }


                string escolha;
                int opcao = 1, posicao = 0;
                bool flag = true;

                do
                {
                    if ((opcao < 1) || (opcao > 5))
                    {
                        Console.WriteLine("\n Opcao invalida.");
                        Console.WriteLine("\n Pressione ENTER para voltar.");
                        Console.ReadKey();
                        opcao = 1;
                    }
                    else
                    {
                        if (opcao == 5)
                        {
                            flag = false;
                            return;
                        }
                        else if (opcao == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("\n Impressao de Produtos");
                            Console.WriteLine(" --------------------------- ");
                            posicao = Produtos.IndexOf(Produtos.First());
                            Console.WriteLine($"\n Produto {posicao + 1}");
                            Console.WriteLine(Impressao(Produtos.First()));
                        }
                        else if (opcao == 4)
                        {
                            Console.Clear();
                            Console.WriteLine("\n Impressao de Produtos");
                            Console.WriteLine(" --------------------------- ");
                            posicao = Produtos.IndexOf(Produtos.Last());
                            Console.WriteLine($"\n Produto {posicao + 1}");
                            Console.WriteLine(Impressao(Produtos.Last()));
                        }
                        else if (opcao == 2)
                        {
                            if (posicao == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("\n Impressao de Produtos");
                                Console.WriteLine(" --------------------------- ");
                                Console.WriteLine("\n Nao ha produto anterior.\n");
                                Console.WriteLine(" --------------------------- ");
                                posicao = Produtos.IndexOf(Produtos.First());
                                Console.WriteLine($"\n Produto {posicao + 1}");
                                Console.WriteLine(Impressao(Produtos.First()));
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\n Impressao de Produtos");
                                Console.WriteLine(" --------------------------- ");
                                posicao--;
                                Console.WriteLine($"\n Produto {posicao + 1}");
                                Console.WriteLine(Impressao(Produtos[posicao]));
                                posicao = Produtos.IndexOf(Produtos[posicao]);
                            }
                        }
                        else if (opcao == 3)
                        {
                            if (posicao == Produtos.IndexOf(Produtos.Last()))
                            {
                                Console.Clear();
                                Console.WriteLine("\n Impressao de Produtos");
                                Console.WriteLine(" --------------------------- ");
                                Console.WriteLine("\n Nao ha proximo produto.\n");
                                Console.WriteLine(" --------------------------- ");
                                Console.WriteLine($"\n Produto {posicao + 1}");
                                Console.WriteLine(Impressao(Produtos.Last()));
                                posicao = Produtos.IndexOf(Produtos.Last());
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\n Impressao de Produtos");
                                Console.WriteLine(" --------------------------- ");
                                posicao++;
                                Console.WriteLine($"\n Produto {posicao + 1}");
                                Console.WriteLine(Impressao(Produtos[posicao]));
                                posicao = Produtos.IndexOf(Produtos[posicao]);
                            }
                        }

                        Console.WriteLine(" ------------------------------------------------------------------ ");
                        Console.WriteLine("\n Navegacao\n");
                        Console.WriteLine(" 1 - Primeiro / 2 - Anterior / 3 - Proximo / 4 - Ultimo / 5 - Sair");
                        Console.Write("\n Escolha: ");
                        escolha = Console.ReadLine();
                        int.TryParse(escolha, out opcao);
                    }

                } while (flag);
            }
            else
            {
                Console.WriteLine("\n Nao ha produtos cadastrados\n");
                Console.WriteLine("\n Pressione ENTER para voltar");
                Console.ReadKey();
            }
        }

        public string Impressao(Produto produto)
        {
            string situacao = "";
            if (produto.Situacao == 'A')
                situacao = "Ativo";
            else if (produto.Situacao == 'I')
                situacao = "Inativo";

            return "\n"
                + "\n Codigo: \t" + produto.CodigoBarras
                + "\n Nome: \t" + produto.Nome
                + "\n Valor Venda: \t" + produto.ValorVenda.ToString("000.#0")
                + "\n Ultima Venda: \t" + produto.UltimaVenda.ToString("dd/MM/yyyy")
                + "\n Data Cadastro: " + produto.DataCadastro.ToString("dd/MM/yyyy")
                + "\n Situacao: \t" + situacao
                + "\n";
        }

        public void Atualizar(string cod, string dataUltimaVenda = null, string situacaoAtualizada = null)
        {
            string produto;
            produto = Buscar(cod);

            if (produto == null)
            {
                Console.WriteLine("\n O produto nao existe.");
                Console.WriteLine("\n Pressione ENTER para voltar");
                Console.ReadKey();
            }
            else
            {
                string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
                Directory.CreateDirectory(caminhoFinal);

                string arquivoFinal = Path.Combine(caminhoFinal, "Cosmetico.dat");

                List<string> Produtos = new List<string>();
                string novoProduto = null;

                if (File.Exists(arquivoFinal))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(arquivoFinal))
                        {
                            string line = sr.ReadLine();
                            do
                            {
                                if (line.Substring(0, 13) != cod)
                                    Produtos.Add(line);

                                line = sr.ReadLine();

                            } while (line != null);
                        }

                        File.Delete(arquivoFinal);

                        if (dataUltimaVenda != null)
                        {
                            novoProduto = produto.Substring(0, 13)
                                + produto.Substring(13, 20)
                                + produto.Substring(33, 5)
                                + dataUltimaVenda.Replace("/", "")
                                + produto.Substring(46, 8)
                                + produto.Substring(54, 1);
                        }
                        else if (situacaoAtualizada != null)
                        {
                            novoProduto = produto.Substring(0, 13)
                                + produto.Substring(13, 20)
                                + produto.Substring(33, 5)
                                + produto.Substring(38, 8)
                                + produto.Substring(46, 8)
                                + situacaoAtualizada;
                        }

                        using (StreamWriter sw = new StreamWriter(arquivoFinal))
                        {
                            Produtos.ForEach(prod => sw.WriteLine(prod));
                            sw.WriteLine(novoProduto);
                        }

                        if (situacaoAtualizada != null)
                        {
                            Console.WriteLine("\n Produto alterado.");
                            Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                            Console.ReadKey();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ex ->" + ex.Message);
                    }
                }
            }
        }

        public string Buscar(string cod, bool remover = false)
        {
            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoFinal = Path.Combine(caminhoFinal, "Cosmetico.dat");

            string produto = null;

            if (File.Exists(arquivoFinal))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivoFinal))
                    {
                        string line = sr.ReadLine();
                        do
                        {
                            if (line.Substring(0, 13) == cod)
                                produto = line;

                            line = sr.ReadLine();

                        } while (line != null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ex ->" + ex.Message);
                }
            }
            return produto;
        }

        public Produto RetornaProduto(string cod)
        {
            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoFinal = Path.Combine(caminhoFinal, "Cosmetico.dat");

            Produto produto = null;

            if (File.Exists(arquivoFinal))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivoFinal))
                    {
                        string line = sr.ReadLine();
                        do
                        {
                            if (line.Substring(0, 13) == cod)
                                produto =
                                    new Produto(
                                        line.Substring(0, 13),
                                        line.Substring(13, 20),
                                        Convert.ToDecimal(line.Substring(33, 5).Insert(3, ",")),
                                        Convert.ToDateTime(line.Substring(38, 8).Insert(2, "/").Insert(5, "/")).Date,
                                        Convert.ToDateTime(line.Substring(46, 8).Insert(2, "/").Insert(5, "/")).Date,
                                        Convert.ToChar(line.Substring(54, 1))
                                        );

                            line = sr.ReadLine();

                        } while (line != null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ex ->" + ex.Message);
                }
            }
            return produto;
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CadastrosBasicos
{
    public class MPrima
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public DateTime UltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public MPrima()
        {

        }

        public MPrima(string id, string nome, DateTime uCompra, DateTime dCadastro, char situacao)
        {
            Id = id;
            Nome = nome;
            UltimaCompra = uCompra;
            DataCadastro = dCadastro;
            Situacao = situacao;
        }


        CadastrosBD cbd = new CadastrosBD();

        public override string ToString()
        {
            return Id
                + Nome.PadLeft(20, ' ')
                + UltimaCompra.ToString("dd/MM/yyyy").Replace("/", "")
                + DataCadastro.ToString("dd/MM/yyyy").Replace("/", "")
                + Situacao;
        }

        public void Menu()
        {
            string escolha;

            do
            {
                Console.Clear();
                Console.WriteLine("\n=============== MATÉRIA-PRIMA ===============");
                Console.WriteLine("1. Cadastrar Matéria-Prima");
                Console.WriteLine("2. Localizar Matéria-Prima");
                Console.WriteLine("3. Imprimir Matérias-Primas");
                Console.WriteLine("4. Alterar Situação da Matéria-Prima");
                Console.WriteLine("---------------------------------------------");
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
                        ImprimirMPrimas();
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
            CadastrosBD cbd = new CadastrosBD();
            MPrima MPrima = new MPrima();

            char sit = 'A';
            string nomeTemp, id;
            bool flag = true;
            int codAtual = 0;
            codAtual++;
            id = "MP" + codAtual.ToString("0000");
            MPrima.Id = id;
            

            do
            {
                Console.Clear();
                Console.WriteLine("\n Cadastro de Materia-prima\n");
                Console.Write(" Nome: ");
                nomeTemp = Console.ReadLine();
                Console.Write(" Situacao (A / I): ");
                sit = char.Parse(Console.ReadLine().ToUpper());

                if (nomeTemp == null)
                {
                    Console.WriteLine(" Nenhum campo podera ser vazio.");
                    Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                    Console.ReadKey();
                }
                else
                {
                    if (nomeTemp.Length > 20)
                    {
                        Console.WriteLine(" Nome invalido. Digite apenas 20 caracteres.");
                        Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }
                    else if ((sit != 'A') && (sit != 'I'))
                    {
                        Console.WriteLine(" Situacao invalida.");
                        Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }
                    else
                    {
                        flag = false;

                        MPrima.Nome = nomeTemp;
                        MPrima.UltimaCompra = DateTime.Now.Date;
                        MPrima.DataCadastro = DateTime.Now.Date;
                        MPrima.Situacao = sit;

                        cbd.RegistraMPrimaBD(id, nomeTemp);
                        //GravarMateriaPrima(MPrima);

                        Console.WriteLine("\n Cadastro de Materia-prima concluido com sucesso!\n");
                        Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                        Console.ReadKey();
                    }
                }

            } while (flag);
        }

        //public void GravarMateriaPrima(MPrima mprima)
        //{
        //    string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
        //    Directory.CreateDirectory(caminhoFinal);

        //    string arquivoFinal = Path.Combine(caminhoFinal, "Materia.dat");

        //    string idMPrima = Path.Combine(caminhoFinal, "IdMPrima.dat");

        //    int codAtual = 0;

        //    try
        //    {
        //        if (!File.Exists(idMPrima))
        //        {
        //            using (StreamWriter sw = new StreamWriter(idMPrima))
        //            {
        //                sw.WriteLine("MP0000");
        //            }
        //        }
        //        else
        //        {
        //            string line;
        //            using (StreamReader sr = new StreamReader(idMPrima))
        //            {
        //                line = sr.ReadLine();
        //            }

        //            codAtual = int.Parse(line.Substring(2, 4));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(" Ex -> " + ex.Message);
        //    }

        //    codAtual++;
        //    mprima.Id = "MP" + codAtual.ToString("0000");

        //    try
        //    {
        //        using (StreamWriter sw = new StreamWriter(idMPrima))
        //        {
        //            sw.WriteLine(mprima.Id);
        //        }

        //        if (!File.Exists(arquivoFinal))
        //        {
        //            using (StreamWriter sw = new StreamWriter(arquivoFinal))
        //            {
        //                sw.WriteLine(mprima.ToString());
        //            }
        //        }
        //        else
        //        {
        //            using (StreamWriter sw = new StreamWriter(arquivoFinal, append: true))
        //            {
        //                sw.WriteLine(mprima.ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Ex -> " + ex.Message);
        //    }
        //}

        public void Localizar()
        {
            string cod;
            MPrima mPrima;

            Console.Clear();
            Console.WriteLine("\n Localizar Materia-prima");
            Console.Write("\n Digite o codigo da materia-prima: ");
            cod = Console.ReadLine();

            mPrima = cbd.BuscaMateriaPrima(cod);

            if (mPrima == null)
            {
                Console.WriteLine("\n A materia-prima nao existe.");
                Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                Console.ReadKey();
            }
            else
            {

                Impressao(mPrima);
                Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                Console.ReadKey();

            }
        }


        public void AlterarSituacao()
        {
            string cod;
            char situacao;
            MPrima mPrima;
            bool flag = true;

            Console.Clear();
            Console.WriteLine("\n Alterar Materia-prima");
            Console.Write("\n Digite o codigo da materia-prima: ");
            cod = Console.ReadLine();

            mPrima = cbd.BuscaMateriaPrima(cod);

            if (mPrima == null)
            {
                Console.WriteLine("\n A materia-prima nao existe.");
                Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                Console.ReadKey();
            }
            else
            {

                Impressao(mPrima);

                do
                {
                    Console.Write("\n Qual a nova situacao da materia-prima (A / I): ");
                    situacao = char.Parse(Console.ReadLine().ToUpper());

                    if ((situacao != 'A') && (situacao != 'I'))
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

                cbd.AtualizaSituacaoMPrima(cod, situacao);
            }
        }


        public string Impressao(MPrima mPrima)
        {
            string situacao = "";
            if (mPrima.Situacao == 'A')
                situacao = "Ativo";
            else if (mPrima.Situacao == 'I')
                situacao = "Inativo";

            return "\n"
                + "\n Codigo: \t" + mPrima.Id
                + "\n Nome: \t" + mPrima.Nome
                + "\n Ultima Venda: \t" + mPrima.UltimaCompra.ToString("dd/MM/yyyy")
                + "\n Data Cadastro: " + mPrima.DataCadastro.ToString("dd/MM/yyyy")
                + "\n Situacao: \t" + situacao
                + "\n";
        }

        public void ImprimirMPrimas()
        {

            bool verifica_tabela = cbd.VerificaTabelaMPrima();
                

            if (verifica_tabela == true)
            {

                List<MPrima> MPrimas = cbd.ListaMPrimas();
                

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
                            Console.WriteLine("\n Impressao de Materias-primas");
                            Console.WriteLine(" --------------------------- ");
                            posicao = MPrimas.IndexOf(MPrimas.First());
                            Console.WriteLine($"\n Materia-prima {posicao + 1}");
                            Console.WriteLine(Impressao(MPrimas.First()));
                        }
                        else if (opcao == 4)
                        {
                            Console.Clear();
                            Console.WriteLine("\n Impressao de Materias-primas");
                            Console.WriteLine(" --------------------------- ");
                            posicao = MPrimas.IndexOf(MPrimas.Last());
                            Console.WriteLine($"\n Materia-prima {posicao + 1}");
                            Console.WriteLine(Impressao(MPrimas.Last()));
                        }
                        else if (opcao == 2)
                        {
                            if (posicao == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("\n Impressao de Materias-primas");
                                Console.WriteLine(" --------------------------- ");
                                Console.WriteLine("\n Nao ha materia-prima anterior.\n");
                                Console.WriteLine(" --------------------------- ");
                                posicao = MPrimas.IndexOf(MPrimas.First());
                                Console.WriteLine($"\n Materia-prima {posicao + 1}");
                                Console.WriteLine(Impressao(MPrimas.First()));
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\n Impressao de Materias-primas");
                                Console.WriteLine(" --------------------------- ");
                                posicao--;
                                Console.WriteLine($"\n Materia-prima {posicao + 1}");
                                Console.WriteLine(Impressao(MPrimas[posicao]));
                                posicao = MPrimas.IndexOf(MPrimas[posicao]);
                            }
                        }
                        else if (opcao == 3)
                        {
                            if (posicao == MPrimas.IndexOf(MPrimas.Last()))
                            {
                                Console.Clear();
                                Console.WriteLine("\n Impressao de Materias-primas");
                                Console.WriteLine(" --------------------------- ");
                                Console.WriteLine("\n Nao ha proxima materia-prima.\n");
                                Console.WriteLine(" --------------------------- ");
                                Console.WriteLine($"\n Materia-prima {posicao + 1}");
                                Console.WriteLine(Impressao(MPrimas.Last()));
                                posicao = MPrimas.IndexOf(MPrimas.Last());
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\n Impressao de Materias-primas");
                                Console.WriteLine(" --------------------------- ");
                                posicao++;
                                Console.WriteLine($"\n Materia-prima {posicao + 1}");
                                Console.WriteLine(Impressao(MPrimas[posicao]));
                                posicao = MPrimas.IndexOf(MPrimas[posicao]);
                            }
                        }

                        Console.WriteLine(" ------------------------------------------------------------------ ");
                        Console.WriteLine("\n Navegacao\n");
                        Console.WriteLine(" 1 - Primeira / 2 - Anterior / 3 - Proxima / 4 - Ultima / 5 - Sair");
                        Console.Write("\n Escolha: ");
                        escolha = Console.ReadLine();
                        int.TryParse(escolha, out opcao);
                    }

                } while (flag);
            }
            else
            {
                Console.WriteLine("\n Nao ha materias-primas cadastradas\n");
                Console.WriteLine("\n Pressione ENTER para voltar");
                Console.ReadKey();
            }
        }

        public MPrima RetornaMateriaPrima(string cod)
        {
            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoFinal = Path.Combine(caminhoFinal, "Materia.dat");

            MPrima MPrima = null;

            if (File.Exists(arquivoFinal))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivoFinal))
                    {
                        string line = sr.ReadLine();
                        do
                        {
                            if (line.Substring(0, 6) == cod)
                                MPrima =
                                    new MPrima(
                                        line.Substring(0, 6),
                                        line.Substring(6, 20),
                                        Convert.ToDateTime(line.Substring(26, 8).Insert(2, "/").Insert(5, "/")).Date,
                                        Convert.ToDateTime(line.Substring(34, 8).Insert(2, "/").Insert(5, "/")).Date,
                                        Convert.ToChar(line.Substring(42, 1))
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
            return MPrima;
        }


      
    }
}


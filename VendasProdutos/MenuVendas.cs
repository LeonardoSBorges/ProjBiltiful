using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasProdutos
{
    public class MenuVendas
    {
        public static void SubMenu()
        {
            new Arquivos();


            bool flag = true;
            do
            {
                Console.Clear();

                Console.WriteLine("=============== VENDAS ===============");
                Console.WriteLine("1. Nova venda");
                Console.WriteLine("2. Consultar Venda");
                Console.WriteLine("3. Consultar Produtos de uma venda");
                Console.WriteLine("4. Excluir Venda");
                Console.WriteLine("0. Voltar");
                string opcao = Console.ReadLine();
                Console.Clear();

                switch (opcao)
                {
                    case "1":
                        NovaVenda();
                        break;

                    case "2":
                        LocalizarVenda();
                        break;

                    case "3":
                        LocalizarItens();
                        break;

                    case "4":
                        ExcluirVenda();
                        break;

                    case "0":
                        flag = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        break;
                }

            } while (flag == true);
        }


        public static void NovaVenda()
        {
            Console.Clear();

            Console.WriteLine("informe o CPF do cliente:");
            string Cliente = Console.ReadLine();

            Console.Clear();

            Venda venda = new Venda();

            venda.Cliente = Cliente;
            venda.DVenda = DateTime.Now.Date;

            Console.Write($"Venda Nº {venda.Id.ToString().PadLeft(5, '0')}\tData: {venda.DVenda.ToString("dd/MM/yyyy")}");
            Console.WriteLine();

            List<ItemVenda> itensVenda = new List<ItemVenda>();

            int itens = 0;
            string escolha;

            do
            {
                Console.WriteLine("\nDigite o Código do Produto:");
                string produto = Console.ReadLine();

                //aqui vai ter que alterar pra fazer a verificação se o produto existe no arquivo de produtos e depois mostrar na tela qual o produto escolhido


                Console.WriteLine("\nInforme a quantidade:");
                int qtd = int.Parse(Console.ReadLine());

                Console.WriteLine("\nInforme o valor do produto: ");
                decimal valor = decimal.Parse(Console.ReadLine());

                //e aqui vai pegar no arquivo de produtos o valor referente ao produto que buscamos. Não vai precisar digitar o valor. 


                Console.Clear();

                itensVenda.Add(new ItemVenda(venda.Id, produto, qtd, valor));

                Console.WriteLine("Id\tProduto\t\tQtd\tV.Unitário\tT.Item");
                Console.WriteLine("------------------------------------------------------");

                decimal valorTotal = 0;

                itensVenda.ForEach(item =>
                {
                    Console.WriteLine(item.ToString());
                    valorTotal += item.TItem;
                    venda.VTotal = valorTotal;
                });

                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine($"\t\t\t\t\t\t{venda.VTotal}");

                if (itens < 2)
                {
                    do
                    {
                        Console.WriteLine("\nAdicionar novo produto?");
                        Console.WriteLine("[ S ] Sim\t[ N ] Não");
                        escolha = Console.ReadLine().ToUpper();

                        Console.Clear();
                    } while (escolha != "S" && escolha != "N");


                    if (escolha == "S")
                        itens++;
                    else
                        break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Seu carrinho está cheio!");
                    break;
                }
            } while (itens < 3);

            do
            {
                Console.Write($"\nVenda Nº {venda.Id.ToString().PadLeft(5, '0')}\tData: {venda.DVenda.ToString("dd/MM/yyyy")}");
                Console.WriteLine("\n\n");

                Console.WriteLine("Id\tProduto\t\tQtd\tV.Unitário\tT.Item");
                Console.WriteLine("------------------------------------------------------");
                itensVenda.ForEach(item => Console.WriteLine(item.ToString()));
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine($"\t\t\t\t\t\t{venda.VTotal}");

                Console.WriteLine("\n\n");

                Console.WriteLine("[ F ] Finalizar venda\t[ C ] Cancelar venda");
                escolha = Console.ReadLine().ToUpper();
            } while (escolha != "F" && escolha != "C");

            if (escolha == "F")
            {
                ItemVenda itemVenda = new ItemVenda();

                itemVenda.Cadastrar(itensVenda);

                venda.Cadastrar();
                Console.WriteLine("venda cadastrada com sucesso!\nPressione ENTER para voltar ao Menu Vendas...");
                Console.ReadKey();
            }
        }

        public static void LocalizarVenda()
        {
            Console.Clear();

            Venda venda = new Venda();

            Console.WriteLine("Informe a venda que deseja buscar: ");
            int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine();
            venda = venda.Localizar(id);

            if (venda != null)
            {
                Console.WriteLine(venda.ToString());
                Console.WriteLine("\nPressione ENTER para voltar ao menu...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("venda não registrada!\nPressione ENTER para voltar ao menu...");
                Console.ReadLine();
            }
        }

        private static void LocalizarItens()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\Everton Fabricio\\source\\repos\\ProjBiltiful\\ProjBiltiful\\bin\\Debug\\net5.0\\ProjBiltiful\\Venda\\ItemVenda.dat");
                //coloquei o caminho inteiro pq não sei como fazer pra chamar caminho do jeito que o Junior fez.

                Console.Write("Pedido Numero: ");
                string busca = Console.ReadLine();
                line = sr.ReadLine();

                while (line != null)
                {
                    if (line.Substring(0, 5) == busca)
                    {
                        Console.WriteLine("Quant\t   Produto\tVal.Unit.  Val.Total");
                        Console.Write(line.Substring(18, 3).TrimStart('0'));
                        Console.Write("\t" + line.Substring(5, 13));
                        Console.Write("\t  " + line.Substring(21, 5).TrimStart('0'));
                        Console.WriteLine("\t     " + line.Substring(26, 5).TrimStart('0'));
                        Console.WriteLine();
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Busca Finalizada.\nPressione ENTER para voltar");
            }
            Console.ReadKey();
        }

        private static void ExcluirVenda()
        {
            Console.WriteLine("Informe a Venda que deseja excluir.");
            Console.Write("Venda Numero: ");
            string busca = Console.ReadLine();
            try //para arquivo Venda.dat
            {
                string ArqCerto = "C:\\Users\\Everton Fabricio\\source\\repos\\ProjBiltiful\\ProjBiltiful\\bin\\Debug\\net5.0\\ProjBiltiful\\Venda\\Venda.dat";
                string ArqTemp = "C:\\Users\\Everton Fabricio\\source\\repos\\ProjBiltiful\\ProjBiltiful\\bin\\Debug\\net5.0\\ProjBiltiful\\Venda\\Temp.dat";
                //Mesma coisa que no localizar item. Eu não sei chamar o caminho do jeito que o Junior fez! Depois que chamar daquele jeito, apaga isso aqui.

                StreamReader sr = new StreamReader(ArqCerto);
                StreamWriter sw = new StreamWriter(ArqTemp);

                string line = sr.ReadLine();

                while (line != null)
                {
                    if (line.Substring(0, 5) != busca)
                    {
                        sw.WriteLine(line);
                    }
                    line = sr.ReadLine();
                }

                sr.Close();
                sw.Close();
                File.Delete(ArqCerto);
                File.Copy(ArqTemp, ArqCerto);
                File.Delete(ArqTemp);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            try //para arquivo ItemVenda.dat
            {
                string ArqCerto = "C:\\Users\\Everton Fabricio\\source\\repos\\ProjBiltiful\\ProjBiltiful\\bin\\Debug\\net5.0\\ProjBiltiful\\Venda\\ItemVenda.dat";
                string ArqTemp = "C:\\Users\\Everton Fabricio\\source\\repos\\ProjBiltiful\\ProjBiltiful\\bin\\Debug\\net5.0\\ProjBiltiful\\Venda\\Temp.dat";
                //Mesma coisa que no localizar item. Eu não sei chamar o caminho do jeito que o Junior fez! Depois que chamar daquele jeito, apaga isso aqui.

                StreamReader sr = new StreamReader(ArqCerto);
                StreamWriter sw = new StreamWriter(ArqTemp);
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (line.Substring(0, 5) != busca)
                    {
                        sw.WriteLine(line);
                    }
                    line = sr.ReadLine();
                }

                sr.Close();
                sw.Close();
                File.Delete(ArqCerto);
                File.Copy(ArqTemp, ArqCerto);
                File.Delete(ArqTemp);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            finally
            {
                Console.WriteLine("Operação realizada com sucesso.\nPressione ENTER para voltar");
            }
            Console.ReadKey();
        }

        public static bool VerificaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

    }
}

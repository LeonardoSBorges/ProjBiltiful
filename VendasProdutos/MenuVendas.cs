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
            string opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("=============== VENDAS ===============");
                Console.WriteLine("1. Nova venda");
                Console.WriteLine("2. Consultar Venda");
                Console.WriteLine("3. Voltar");

                switch (opcao = Console.ReadLine())
                {
                    case "1":
                        NovaVenda();
                        break;

                    case "2":
                        LocalizarVenda();
                        break;

                    case "3":
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        break;
                }

                Console.ReadKey();

            } while (opcao != "3");
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

                Console.WriteLine("\nInforme a quantidade:");
                int qtd = int.Parse(Console.ReadLine());

                Console.WriteLine("\nInforme o valor do produto: ");
                decimal valor = decimal.Parse(Console.ReadLine());

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

                if (itens < 3)
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
                    Console.WriteLine("Seu carrinho está cheio!");
                    break;
                }
            } while (itens < 3);

            do
            {
                Console.Write($"Venda Nº {venda.Id.ToString().PadLeft(5, '0')}\tData: {venda.DVenda.ToString("dd/MM/yyyy")}");
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
            }


        }

        public static void LocalizarVenda()
        {
            Console.Clear();

            Venda venda = new Venda();

            Console.WriteLine("Informe o ID da venda que deseja buscar: ");
            int.TryParse(Console.ReadLine(), out int id);

            venda = venda.Localizar(id);

            if (venda != null)
                Console.WriteLine(venda.ToString());
            else
                Console.WriteLine("venda não registrada!");
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

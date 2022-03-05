using System;
using System.Collections.Generic;
using System.Globalization;
using VendasProdutos;

namespace ProjBiltiful
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cultureInformation = new CultureInfo("pt-BR");
            cultureInformation.NumberFormat.CurrencySymbol = "R$";
            CultureInfo.DefaultThreadCurrentCulture = cultureInformation;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInformation;

            new Arquivos();


            string opcao;

            do
            {
                switch (opcao = Menu())
                {
                    case "1":
                        NovaVenda();
                        break;

                    case "2":
                        break;

                    case "3":
                        Environment.ExitCode = 0;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        break;
                }

            } while (opcao != "3");
        }

        public static string Menu()
        {
            Console.WriteLine("========== Vendas ==========");
            Console.WriteLine("[1] Nova venda");
            Console.WriteLine("[2] Consultar Venda");
            Console.WriteLine("[3] Voltar");

            return Console.ReadLine();
        }

        public static void NovaVenda()
        {
            int id = 1;

            Venda venda = new Venda();
            ItemVenda itemVenda = new ItemVenda();

            List<ItemVenda> itens = new List<ItemVenda>();

            for (int i = 0; i < 3; i++)
            {
                Console.Clear();

                Console.WriteLine("Informe o produto: ");
                string produto = Console.ReadLine();

                Console.WriteLine("Informe a quantidade do produto: ");
                int quantidade = int.Parse(Console.ReadLine());

                Console.WriteLine("Informe o valor do produto: ");
                decimal vUnitario = Decimal.Parse(Console.ReadLine());

                itens.Add(new ItemVenda(id, produto, quantidade, vUnitario));
            }

            itens.ForEach(item => venda.VTotal += item.TItem);

            venda.Id = id;
            venda.Cliente = "123.456.789-00";
            venda.DVenda = DateTime.Now;

            itemVenda.Cadastrar(itens);

            venda.Cadastrar();
        }
    }
}

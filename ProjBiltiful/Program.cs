using System;
using System.IO;
using VendasProdutos;
using CadastrosBasicos;
using ComprasMateriasPrimas;
using CadastrosBasicos.ManipulaArquivos;
using ProducaoCosmeticos;
using System.Globalization;

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

            string escolha;

            do
            {
                Console.Clear();

                Console.WriteLine("=============== MENU ===============");
                Console.WriteLine("1. Cadastros");
                Console.WriteLine("2. Produção");
                Console.WriteLine("3. Compras");
                Console.WriteLine("4. Vendas");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        Environment.Exit(0);
                        break;

                    case "1":
                        MenuCadastros.SubMenu();
                        break;

                    case "2":
                            new Producao().SubMenu();
                        break;

                    case "3":
                            Compra.SubMenu();
                        break;

                    case "4":
                       
                            MenuVendas.SubMenu();
                       
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.WriteLine("\nPressione ENTER para voltar ao menu");
                        break;
                }

            } while (escolha != "0");
        }
    }
}
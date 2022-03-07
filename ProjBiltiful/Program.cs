
using System;
using System.IO;
using VendasProdutos;
using CadastrosBasicos;
using ProducaoCosmeticos;
using System.Globalization;

namespace ProjBiltiful
{
    internal class Program
    {
        public static void GerarPastas()
        {
            string caminhoInicial = Directory.GetCurrentDirectory();
            Console.WriteLine(caminhoInicial);
            string caminhoFinal = Path.Combine(caminhoInicial, "ProjBiltiful");
            Directory.CreateDirectory(caminhoFinal);

            string pastaProducao = Path.Combine(caminhoFinal, "ProducaoCosmeticos");
            Directory.CreateDirectory(pastaProducao);

        }

        static void Main(string[] args)
        {

            var cultureInformation = new CultureInfo("pt-BR");
            cultureInformation.NumberFormat.CurrencySymbol = "R$";
            CultureInfo.DefaultThreadCurrentCulture = cultureInformation;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInformation;
          
            GerarPastas();

            //Este menu sera utilizado para testes
            int value = -1;
            while (value != 0)
            {
                Console.Clear();
                Console.Write(@"=============== BITIFUL ===============
1. Cadastros
2. Vendas
2. Compra de Materia-Prima
3. Producao
0 - Sair
Insira uma opcao valida: 
");
                value = int.Parse(Console.ReadLine());



                switch (value)
                {
                    case 0:
                        Environment.Exit(0);
                        break;

                    case 1:
                        MenuCadastros.SubMenu();
                        break;

                    case 2:
                        MenuVendas.SubMenu();
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida");
                        Console.ReadKey();
                        break;
                }
            } while (value != 0);
        }
    }
}
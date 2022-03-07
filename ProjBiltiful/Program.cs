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
                Console.Write(@"1. Cadastros
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
                        // sair
                        break;
                    case 1:
                        //Cadastrar
                        break;
                    case 2:
                        break;
                    case 3:
                        break;

                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

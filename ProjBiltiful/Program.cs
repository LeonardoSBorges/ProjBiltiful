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

            string pastaMateriaPrima = Path.Combine(caminhoFinal, "MateriaPrima");
            Directory.CreateDirectory(pastaMateriaPrima);

            string pastaProduto = Path.Combine(caminhoFinal, "Produto");
            Directory.CreateDirectory(pastaProduto);

        }

        static void Main(string[] args)
        {
            var cultureInformation = new CultureInfo("pt-BR");
            cultureInformation.NumberFormat.CurrencySymbol = "R$";
            CultureInfo.DefaultThreadCurrentCulture = cultureInformation;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInformation;

            

            //Produto a = new Produto();
            //a.Menu();
            MPrima b = new MPrima();
            b.Menu();

            /*decimal valor = 10.12m;
            Console.WriteLine(valor.ToString("000.#0"));
            Console.ReadKey();*/


            /*int value = -1;
            while (value != 0)
            {
                Console.Write(@"============= BITIFUL =============
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
                        // sair
                        break;
                    case 1:
                        //Cadastrar
                        MenuCadastros.SubMenu();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;

                }


            }*/
        }
    }
}

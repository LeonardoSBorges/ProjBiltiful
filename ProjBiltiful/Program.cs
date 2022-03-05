using System;
using VendasProdutos;
using CadastrosBasicos;
using ProducaoCosmeticos;
using System.Globalization;

namespace ProjBiltiful
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Este menu sera utilizado para testes
            int value = -1;
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

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

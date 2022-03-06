using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasProdutos
{
    public class MenuVendas
    {
        public static void SubMenu()
        {
            Venda venda = new Venda();
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
                        venda.NovaVenda();
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
    }
}

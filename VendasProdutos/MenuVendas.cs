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

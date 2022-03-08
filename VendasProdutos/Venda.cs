using System;
using System.Collections.Generic;
using System.IO;

namespace VendasProdutos
{
    public class Venda
    {
        public static Arquivos caminho = new Arquivos();

        public int Id { get; set; }
        public string Cliente { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }

        public Venda()
        {
            Id = NovoIdVenda();
            ValorTotal = 0;
        }

        public Venda(int id, string cliente, DateTime dataVenda, decimal vTotal)
        {
            Id = id;
            Cliente = cliente;
            DataVenda = dataVenda;
            ValorTotal = vTotal;
        }

        public override string ToString()
        {
            return $"Venda Nº {Id.ToString().PadLeft(5, '0')}\tData: {DataVenda.ToString("dd/MM/yyyy")}\nCliente: {Cliente}\nTotal da Venda: {ValorTotal.ToString("00000.00").TrimStart('0')}";
        }

        public int NovoIdVenda()
        {
            try
            {
                return File.ReadAllLines(caminho.ArquivoVenda).Length + 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return -1;
        }

        public void Cadastrar()
        {
            try
            {
                StreamWriter sw = new StreamWriter(caminho.ArquivoVenda, append: true);

                sw.WriteLine(Id.ToString().PadLeft(5, '0') + Cliente.Replace(".", "").Replace("-", "") + DataVenda.ToString("dd/MM/yyyy").Replace("/", "") + ValorTotal.ToString("00000.00").Replace(",", ""));

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public Venda Localizar(int id)
        {
            string linha = LocalizarVendaPorId(id);

            if (linha != null)
            {
                string idVenda = linha.Substring(0, 5);
                string cliente = linha.Substring(5, 11);
                string data = linha.Substring(16, 8);
                string vtotal = linha.Substring(24, 7);

                DateTime.TryParse(data.Insert(2, "/").Insert(5, "/"), out DateTime dt);

                Venda venda = new Venda(int.Parse(idVenda), cliente.Insert(3, ".").Insert(7, ".").Insert(11, "-"), dt, Decimal.Parse(vtotal.Insert(vtotal.Length - 2, ",")));

                return venda;
            }

            return null;
        }

        public string LocalizarVendaPorId(int id)
        {
            string[] dados = File.ReadAllLines(caminho.ArquivoVenda);

            int minimo = 0;
            int maximo = dados.Length;
            int medio;

            while (minimo <= maximo)
            {
                medio = (minimo + maximo) / 2;

                if (id > medio)
                    minimo = medio + 1;
                else if (id < medio)
                    maximo = medio - 1;
                else
                    return (medio != 0) ? dados[medio - 1] : null;
            }

            return null;
        }

        public void ImpressaoPorRegistro()
        {
            Console.Clear();

            if (File.ReadAllLines(caminho.ArquivoItemVenda).Length == 0)
            {
                Console.Clear();
                Console.WriteLine("Não ha vendas para exibir\nPressione ENTER para voltar...");
                Console.ReadLine();
                return;
            }

            string[] dados = File.ReadAllLines(caminho.ArquivoVenda);

            var i = 0;
            string choice;
            ItemVenda itemVenda = new ItemVenda();

            do
            {
                string idVenda = dados[i].Substring(0, 5);
                string cliente = dados[i].Substring(5, 11);
                string data = dados[i].Substring(16, 8);
                string vtotal = dados[i].Substring(24, 7);

                DateTime.TryParse(data.Insert(2, "/").Insert(5, "/"), out DateTime dt);

                Venda venda = new Venda(int.Parse(idVenda), cliente.Insert(3, ".").Insert(7, ".").Insert(11, "-"), dt, Decimal.Parse(vtotal.Insert(vtotal.Length - 2, ",")));


                List<ItemVenda> itens = itemVenda.Localizar(venda.Id);

                Console.Write($"\nVenda Nº {venda.Id.ToString().PadLeft(5, '0')}\tData: {venda.DataVenda.ToString("dd/MM/yyyy")}");
                Console.WriteLine("\n\n");

                Console.WriteLine("Id\tProduto\t\tQtd\tV.Unitário\tT.Item");
                Console.WriteLine("------------------------------------------------------");
                itens.ForEach(item => Console.WriteLine(item.ToString()));
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine($"\t\t\t\t\t\t{venda.ValorTotal.ToString("#.00")}");

                Console.WriteLine("\n\n");
                Console.WriteLine("1 - Proximo\t2 - Anterior\t3 - Primeiro\t4 - Ultimo\t0 - Cancelar");
                choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        if (i == dados.Length - 1)
                            i = dados.Length - 1;
                        else
                            i++;
                        break;

                    case "2":
                        if (i == 0)
                            i = 0;
                        else
                            i--;
                        break;

                    case "3":
                        i = 0;
                        break;

                    case "4":
                        i = dados.Length - 1;
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção invalida. Tente novamente.");
                        break;
                }
            } while (choice != "0");
        }
    }
}

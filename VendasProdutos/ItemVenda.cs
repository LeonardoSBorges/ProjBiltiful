using System;
using System.Collections.Generic;
using System.IO;

namespace VendasProdutos
{
    public class ItemVenda
    {
        private static Arquivos caminho = new Arquivos();

        public int Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal TotalItem { get; set; }

        public ItemVenda() { }

        public ItemVenda(int id, string produto, int quantidade, decimal valorUnitario)
        {
            Id = id;
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = quantidade * valorUnitario;
        }

        public override string ToString()
        {
            return $"{Id.ToString().PadLeft(5, '0')}\t{Produto}\t{Quantidade.ToString().PadLeft(3, '0')}\t{ValorUnitario.ToString("000.00").TrimStart('0')}\t\t{TotalItem.ToString("0000.00").TrimStart('0')}";
        }

        public void Cadastrar(List<ItemVenda> itens)
        {
            try
            {
                StreamWriter sw = new StreamWriter(caminho.ArquivoItemVenda, append: true);

                itens.ForEach(item =>
                {
                    string linha = item.Id.ToString().PadLeft(5, '0') + item.Produto + item.Quantidade.ToString().PadLeft(3, '0') + item.ValorUnitario.ToString("000.00").Replace(",", "") + item.TotalItem.ToString("0000.00").Replace(",", "");
                    sw.WriteLine(linha);
                });

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public List<ItemVenda> Localizar(int idVenda)
        {
            try
            {
                StreamReader sr = new StreamReader(caminho.ArquivoItemVenda);

                List<ItemVenda> itens = new List<ItemVenda>();

                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    int.TryParse(line.Substring(0, 5).TrimStart('0'), out int id);

                    if (id == idVenda)
                    {
                        string produto = line.Substring(5, 13);
                        string quantidade = line.Substring(18, 3);
                        string valorUnitario = line.Substring(21, 5);

                        Decimal.TryParse(valorUnitario.Insert(valorUnitario.Length - 2, ","), out decimal vUnitario);

                        itens.Add(new ItemVenda(id, produto, int.Parse(quantidade), vUnitario));
                    }
                }

                sr.Close();

                return itens;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return null;
        }
    
    }
}

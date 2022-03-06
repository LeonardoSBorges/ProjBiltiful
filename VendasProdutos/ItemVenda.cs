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
        public int Qtd { get; set; }
        public decimal VUnitario { get; set; }
        public decimal TItem { get; set; }

        public ItemVenda() { }

        public ItemVenda(int id, string produto, int qtd, decimal vUnitario)
        {
            Id = id;
            Produto = produto;
            Qtd = qtd;
            VUnitario = vUnitario;
            TItem = qtd * vUnitario;
        }

        public override string ToString()
        {
            return $"{Id}\t{Produto}\t{Qtd}\t{VUnitario}\t\t{TItem}";
        }

        public void Cadastrar(List<ItemVenda> itens)
        {
            try
            {
                StreamWriter sw = new StreamWriter(caminho.ArquivoItemVenda, append: true);

                itens.ForEach(item =>
                {
                    string linha = item.Id.ToString().PadLeft(5, '0') + item.Produto + item.Qtd.ToString().PadLeft(3, '0') + item.VUnitario.ToString("N2").PadLeft(5, '0') + item.TItem.ToString("N2").PadLeft(5, '0');
                    sw.WriteLine(linha);
                });

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void Localizar()
        {
        }

        public void Excluir()
        {
        }

        public void ImpressaoPorRegistro()
        {
        }
    }
}

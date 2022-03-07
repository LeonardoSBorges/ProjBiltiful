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

        }


    }
}

using System;
using System.IO;

namespace VendasProdutos
{

    public class Venda
    {
        private static string caminho = @"ProjBiltiful\\Venda\\Venda.dat";

        public int Id { get; set; }
        public string Cliente { get; set; }
        public DateTime DVenda { get; set; }
        public decimal VTotal { get; set; }

        public Venda()
        {
            VTotal = 0;
        }
        public Venda(int id, string cliente, DateTime dVenda, decimal vTotal)
        {
            Id = id;
            Cliente = cliente;
            DVenda = dVenda;
            VTotal = vTotal;
        }

        public override string ToString()
        {
            return $"{Id}\t{DVenda.ToString("dd/MM/yyyy")}\n{Cliente}\n{VTotal}";
        }

        public void Cadastrar()
        {
            try
            {
                StreamWriter sw = new StreamWriter(caminho, append: true);

                sw.WriteLine(Id.ToString().PadLeft(5, '0') + Cliente + DVenda.ToString("dd/MM/yyyy") + VTotal);

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public Venda Localizar(int id)
        {
            try
            {
                string linha = BuscaBinaria(id);

                if (linha != null)
                {
                    string idVenda = linha.Substring(0, 5);
                    string cliente = linha.Substring(5, 14);
                    string data = linha.Substring(19, 10);
                    string vtotal = linha.Substring(29, 5);

                    Venda venda = new Venda(int.Parse(idVenda), cliente, DateTime.Parse(data), Decimal.Parse(vtotal));

                    return venda;
                }

                return null;

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return null;
        }

        public string BuscaBinaria(int id)
        {
            try
            {
                string[] dados = File.ReadAllLines(caminho);

                int minimo = 0;
                int maximo = dados.Length - 1;
                int medio;

                while (minimo <= maximo)
                {
                    medio = (minimo + maximo) / 2;

                    if (id > medio)
                        minimo = medio + 1;
                    else if (id < medio)
                        maximo = medio - 1;
                    else
                        return dados[medio - 1];
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return null;
        }

        public void Excluir()
        {
        }

        public void ImpressaoPorRegistro()
        {
        }
    }
}

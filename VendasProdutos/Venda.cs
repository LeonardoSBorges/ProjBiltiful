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

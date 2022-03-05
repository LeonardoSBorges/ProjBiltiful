using System;
using System.IO;

namespace VendasProdutos
{
    public class Venda
    {
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
            if (!Directory.Exists("database"))
            {
                Directory.CreateDirectory("database");
                File.Create("database\\Venda.dat").Close();
            }

            try
            {
                StreamWriter sw = new StreamWriter("database\\Venda.dat", append: true);

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

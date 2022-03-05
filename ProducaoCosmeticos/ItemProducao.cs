using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducaoCosmeticos
{
    internal class ItemProducao
    {
        public int id { get; set; }
        public DateTime dproducao { get; set; }
        public string mprima { get; set; }
        public decimal qtdmp { get; set; }


        public void CadastrarIdproducao()
        {


        }


        public void QuantidadeMP()
        {
         
        }

        public void SalvarItemProducao()
        {
            try
            {
                StreamWriter sw = new StreamWriter("ItemProducao.dat", append: true);
                sw.WriteLine($"{id};{mprima};{qtdmp};{dproducao.ToString("dd/MM/yyyy")}");
                sw.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine("Exception: " + e.ToString());
            }
        }



    }



}

using CadastrosBasicos;
using System;
using System.IO;
using System.Collections.Generic;

namespace ProducaoCosmeticos
{
    public class ItemProducao
    {
        #region Atributos e Propriedades da Classe ItemProdução

        public string Id { get; set; }
        public DateTime dproducao { get; set; }
        public string mprima { get; set; }
        public decimal qtdmp { get; set; }


        #endregion

        #region Construtor

        public ItemProducao()
        {

        }

        public ItemProducao(string id, DateTime dproducao, string mprima, decimal qtdmp)
        {
            Id = id;
            this.dproducao = dproducao;
            this.mprima = mprima;
            this.qtdmp = qtdmp;
        }

        #endregion

        #region Métodos 

        public void BuscarMateriaPrima()
        {
            string caminhoInicial = Directory.GetCurrentDirectory();
            Console.WriteLine(caminhoInicial);
            string caminhoFinal = Path.Combine(caminhoInicial, "\\ProjBiltiful\\");
            Directory.CreateDirectory(caminhoFinal);

            string pastaProducao = Path.Combine(caminhoFinal, "ProducaoCosmeticos\\");
            Directory.CreateDirectory(pastaProducao);

            string arquivoFinal = Path.Combine(pastaProducao + "ItemProducao.dat");

            List<MPrima> ListaMateriasPrimas = new List<MPrima>();
            if (File.Exists(arquivoFinal))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivoFinal))
                    {
                        string line = sr.ReadLine();
                        do
                        {
                            if (line.Substring(54, 1) != "I")
                            {
                                ListaMateriasPrimas.Add(
                                    new MPrima()
                                    );
                            }
                        } while (line != null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ex -> " + ex.Message);
                }
            }

            #endregion
        }
    }
}
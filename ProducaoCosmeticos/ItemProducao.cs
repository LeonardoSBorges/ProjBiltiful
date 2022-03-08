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
        public string DataProducao { get; set; }
        public string MateriaPrima { get; set; }
        public float QuantidadeMateriaPrima { get; set; }

        #endregion

        #region Construtor

        public ItemProducao()
        { }

        public ItemProducao(string id, string dataProducao, string materiaPrima, float quantidadeMateriaPrima)
        {
            Id = id;
            DataProducao = dataProducao;
            MateriaPrima = materiaPrima;
            QuantidadeMateriaPrima = quantidadeMateriaPrima;
        }

        #endregion

        #region Métodos 

        public string BuscarProducao(string codigoProducao)
        {
            string codigoProducaoEncontrado = null;
            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoProducao = Path.Combine(caminhoFinal, "Producao.dat");
            List<string> ListaProducao = new List<string>();
            if (File.Exists(arquivoProducao))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivoProducao))
                    {
                        string line = sr.ReadLine();
                        do
                        {
                            if (line.Substring(0, 5) == codigoProducao)
                            {
                                ListaProducao.Add(line.Substring(0, 5));
                                if (line.Substring(0, 6) == codigoProducao)
                                {
                                    codigoProducaoEncontrado = line.Substring(0, 5);
                                }
                            }
                            line = sr.ReadLine();
                        } while (line != null);
                        sr.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ex -> " + ex.Message);
                }
            }
            return codigoProducaoEncontrado;
        }

        public void GravarItemProducao(List<ItemProducao> itens)
        {
            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            string arquivoItemProducao = Path.Combine(caminhoFinal, "ItemProducao.dat");


            if (!Directory.Exists(caminhoFinal))
            {
                Directory.CreateDirectory(caminhoFinal);
                File.Create(arquivoItemProducao).Close();
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(arquivoItemProducao, append: true))
                {
                    itens.ForEach(item =>
                    {
                        sw.WriteLine(item.ToString());
                    });

                    sw.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }

        }

        #endregion


        public override string ToString()
        {
            return Id + DataProducao.Replace("/", "") + MateriaPrima + QuantidadeMateriaPrima.ToString("00000");
        }

    }
}
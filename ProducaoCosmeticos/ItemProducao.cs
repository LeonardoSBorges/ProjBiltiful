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
        public decimal QuantidadeMateriaPrima { get; set; }

        #endregion

        #region Construtor

        public ItemProducao()
        {
            BuscarMateriaPrima(MateriaPrima);
            CadastrarItemProducao();
            GravarItemProducao();
        }

        public ItemProducao(string id, string dataProducao, string materiaPrima, decimal quantidadeMateriaPrima)
        {
            Id = id;
            DataProducao = dataProducao;
            MateriaPrima = materiaPrima;
            QuantidadeMateriaPrima = quantidadeMateriaPrima;
        }

        #endregion

        #region Métodos 

        public void CadastrarItemProducao()
        {
            int quantidadeMateriasPrimasUtilizadas = 0;
            string opcao = "";
            string codigoMateriaPrima;
            decimal quantidadeMateriaPrimaAux;
            do
            {
                do
                {
                    Console.WriteLine("Digite o Código da Matéria-Prima");
                    codigoMateriaPrima = Console.ReadLine();
                    if (BuscarMateriaPrima(codigoMateriaPrima) == null)
                    {
                        Console.WriteLine("Código Inválido");
                    }
                    else
                    {
                        MateriaPrima = codigoMateriaPrima;
                    }

                } while (BuscarMateriaPrima(codigoMateriaPrima) == null);
                do
                {
                    Console.WriteLine("Digite a Quantidade de Matéria-Prima");
                    quantidadeMateriaPrimaAux = decimal.Parse(Console.ReadLine());
                    if (quantidadeMateriaPrimaAux > 0 && quantidadeMateriaPrimaAux < 1000)
                    {
                        QuantidadeMateriaPrima = quantidadeMateriaPrimaAux;
                    }
                    else
                    {
                        Console.WriteLine("Não é possível adicionar essa quantidade de matéria-prima");
                    }
                } while (quantidadeMateriaPrimaAux > 1000);

                quantidadeMateriasPrimasUtilizadas++;
                if (quantidadeMateriasPrimasUtilizadas < 3)
                {
                    Console.WriteLine("Deseja adicionar uma nova matéria-prima\n1 - Sim\n2 - Não");
                    opcao = Console.ReadLine();
                }
            } while ((opcao != "2") && (quantidadeMateriasPrimasUtilizadas < 3));
        }

        public string BuscarMateriaPrima(string codigoMateriaPrima)
        {
            string codigoMateriaPrimaEncontrado = null;
            string caminhoInicial = Directory.GetCurrentDirectory();
            string caminhoFinal = Path.Combine(caminhoInicial, "ProjBiltiful\\");
            //Directory.CreateDirectory(caminhoFinal);

            string PastaMateriaPrima = Path.Combine(caminhoFinal, "MateriaPrima\\");
            string arquivoMateriaPrima = Path.Combine(PastaMateriaPrima, "Materia.dat");

            List<string> ListaMateriasPrimas = new List<string>();
            if (File.Exists(arquivoMateriaPrima))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivoMateriaPrima))
                    {
                        string line = sr.ReadLine();
                        do
                        {
                            if (line.Substring(42, 1) != "I")
                            {
                                ListaMateriasPrimas.Add(line.Substring(0, 6));
                                if (line.Substring(0, 6) == codigoMateriaPrima)
                                {
                                    codigoMateriaPrimaEncontrado = line.Substring(0, 6);
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
            return codigoMateriaPrimaEncontrado;
        }


        public void GravarItemProducao()
        {
            //string itemProducao = Id + DataProducao.Replace("/", "") + MateriaPrima + QuantidadeMateriaPrima;
            string caminhoInicial = Directory.GetCurrentDirectory();
            string caminhoFinal = Path.Combine(caminhoInicial, "ProjBiltiful\\");

            string PastaItemProducao = Path.Combine(caminhoFinal, "ProducaoCosmeticos\\");
            string arquivoMateriaPrima = Path.Combine(PastaItemProducao, "ItemProducao.dat");
            if (File.Exists(arquivoMateriaPrima))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(arquivoMateriaPrima, append: true))
                    {
                        //sw.WriteLine(itemProducao);
                        //StreamWriter sw = new StreamWriter("ItemProducao.dat", append: true;
                        sw.Close();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.ToString());
                }
            }
        }

        #endregion


        public override string ToString()
        {
            return Id + DataProducao.Replace("/", "") + MateriaPrima + QuantidadeMateriaPrima.ToString("00000");
        }
    }
}
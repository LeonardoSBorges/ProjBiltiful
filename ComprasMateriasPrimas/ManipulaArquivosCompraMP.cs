using System.Collections.Generic;
using System.IO;

namespace ComprasMateriasPrimas
{
    public class ManipulaArquivosCompraMP
    {
        public static string Caminho { get; set; }
        public static string ArquivoCompra { get; set; }
        public static string ArquivoItemCompra { get; set; }
        public ManipulaArquivosCompraMP()
        {
            Caminho = GetCaminhoDiretorio();
            ArquivoCompra = GetCaminhoArquivo("Compra.dat");
            ArquivoItemCompra = GetCaminhoArquivo("ItemCompra.dat");
        }
        public static void Salvar(List<ItemCompra> itensCompra)
        {
            StreamWriter sw = File.AppendText(ArquivoItemCompra);
            foreach (ItemCompra item in itensCompra)
            {
                string linha = $"{item.Id,-5}" +
                    $"{item.DCompra:ddMMyyyy}" +
                    $"{item.MPrima,-6}" +
                    $"{item.Qtd.ToString().Replace(".", "").Replace(",", ""), -5}" +
                    $"{item.VUnitario.ToString().Replace(".", "").Replace(",", ""), -5}" +
                    $"{item.TItem.ToString().Replace(".", "").Replace(",", ""), -6}";

                sw.WriteLine(linha);
            }
            sw.Close();
        }
            
        static string GetCaminhoDiretorio()
        {
            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "ProjBiltiful", "Compra");
            if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
            return caminho;
        }

        string GetCaminhoArquivo(string arquivo)
        {
            string arquivoDat = Path.Combine(Caminho, arquivo);
            if (!File.Exists(arquivoDat))
                File.Create(arquivoDat).Close();
            return arquivoDat;
        }

    }
}

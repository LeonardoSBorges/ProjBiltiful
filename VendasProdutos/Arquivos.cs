using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasProdutos
{
    public class Arquivos
    {
        public string CaminhoVenda { get; set; }
        public string ArquivoVenda { get; set; }
        public string ArquivoItemVenda { get; set; }

        public Arquivos()
        {
            CaminhoVenda = Caminho();
            ArquivoVenda = VerificaArquivo("Venda.dat");
            ArquivoItemVenda = VerificaArquivo("ItemVenda.dat");
        }

        private string Caminho()
        {
            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            return caminho;
        }

        private string VerificaArquivo(string arquivo)
        {
            string arquivoDat = Path.Combine(CaminhoVenda, arquivo);

            if (!File.Exists(arquivoDat))
                File.Create(arquivoDat).Close();

            return arquivoDat;
        }
    }
}

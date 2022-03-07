using System;
using System.IO;
using CadastrosBasicos;

namespace ComprasMateriasPrimas
{
    public class ManipulaArquivosCompraMP
    {
        public string Diretorio { get; set; }
        public string CaminhoCompra { get; set; }
        public string CaminhoItemCompra { get; set; }

        public ManipulaArquivosCompraMP()
        {
            Diretorio = SetCaminhoDiretorio();
            CaminhoCompra = SetCaminhoArquivo("Compra.dat");
            CaminhoItemCompra = SetCaminhoArquivo("ItemCompra.dat");
        }

        public void Salvar(Compra compra)
        {

        }

        public void Procura(int procuraId)
        {
            Compra procura;
            try
            {   
                using (StreamReader sr = new (CaminhoCompra))
                {
                    string linha;
                    int id;
                    while ((linha = sr.ReadLine()) != null || (id = Compra.ExtrairId(linha)) == procuraId);

                    DateTime dCompra = Compra.ExtrairDCompra(linha);
                    string fornecedor = Read.ProcurarFornecedor(cnpj);

                    procura = new Compra(id, dCompra, );
                }
            }
            catch (Exception ex)
            {

            }
            return procura;
        }

        static string SetCaminhoDiretorio()
        {
            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "ProjBiltiful", "Compra");
            if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho);
            return caminho;
        }

        string SetCaminhoArquivo(string arquivo)
        {
            string arquivoDat = Path.Combine(Diretorio, arquivo);
            if (!File.Exists(arquivoDat)) 
                File.Create(arquivoDat).Close();
            return arquivoDat;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using CadastrosBasicos;
using CadastrosBasicos.ManipulaArquivos;
using System.Linq;

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
            try
            {
                using (StreamWriter sw = new(CaminhoCompra, append: true))
                {
                    sw.WriteLine(compra);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível escrever no arquivo de Compras: " + ex.Message);
            }
        }

        public void Salvar(List<ItemCompra> itensCompra)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(CaminhoItemCompra))
                {
                    itensCompra.ForEach(item => sw.WriteLine(item));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível escrever no arquivo ItemCompra.dat: " + ex.Message);
            }
        }

        public Compra Procura(int idProcura)
        {
            Compra procura;
            try
            {   
                using (StreamReader sr = new (CaminhoCompra))
                {
                    string linha;
                    if (sr.ReadToEnd() != string.Empty) while ((linha = sr.ReadLine()) != null || Compra.ExtrairId(linha) == idProcura);
                    else linha = string.Empty;
                    procura = linha != string.Empty && !new Read().ProcurarCNPJBloqueado(Compra.ExtrairCNPJ(linha)) ? Compra.ExtrairCompra(linha) : null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel ler o arquivo Compra.dat: " + ex.Message);
                procura = null;
            }
            return procura;
        }

        public List<Compra> PegarTodasAsCompras()
        {
            var arquivoCompleto = new List<Compra>();
            var dadosArquivo = File.ReadAllLines(CaminhoCompra).ToList();
            if (dadosArquivo.Count != 0) dadosArquivo.ForEach(linha => arquivoCompleto.Add(Compra.ExtrairCompra(linha)));
            return arquivoCompleto;
        }

        static string SetCaminhoDiretorio()
        {
            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
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

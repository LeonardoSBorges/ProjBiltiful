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

        public int PegarUltimoId() => ProcuraUltimo();

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
            Compra procura = null;
            string linha;
            try
            {
                using (StreamReader sr = new(CaminhoCompra))
                {
                    if (File.ReadAllLines(CaminhoCompra).Length != 0)
                    {
                        while ((linha = sr.ReadLine()) != null)
                        {
                            if (Compra.ExtrairId(linha) == idProcura && !new Read().ProcurarCNPJBloqueado(Compra.ExtrairCNPJ(linha)))
                            {
                                procura = Compra.ExtrairCompra(linha);
                                return procura;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Arquivo vazio");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel ler o arquivo Compra.dat: " + ex.Message);
            }
            return procura;
        }

        public int ProcuraUltimo()
        {
            try
            {
                return File.ReadAllLines(CaminhoCompra).Length + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel ler o arquivo Compra.dat: " + ex.Message);
            }

            return 1;
        }

        public List<Compra> PegarTodasAsCompras()
        {
            var arquivoCompleto = new List<Compra>();
            var dadosArquivo = File.ReadAllLines(CaminhoCompra).ToList();
            if (dadosArquivo.Count != 0 || dadosArquivo != null) dadosArquivo.ForEach(linha => arquivoCompleto.Add(Compra.ExtrairCompra(linha)));
            else Console.WriteLine("Arquivo nao encontrado ou vazio!");
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

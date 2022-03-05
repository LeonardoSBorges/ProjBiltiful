using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastrosBasicos;

namespace ProjBiltiful
{
    public class Arquivos
    {
        public string caminhoFinal { get; set; }
        public string pastaCliente { get; set; }
        public string pastaFornecedor { get; set; }
        public string pastaMateriaPrima { get; set; }
        public string pastaProduto { get; set; }
        public string pastaRisco { get; set; }
        public string pastaBloqueado { get; set; }
        public string pastaVenda { get; set; }

        public Arquivos()
        {
            GerarPastas();
        }

        public void GerarPastas()
        {
            string caminhoInicial = Directory.GetCurrentDirectory();
            caminhoFinal = Path.Combine(caminhoInicial, "ProjBiltiful");
            Directory.CreateDirectory(caminhoFinal);

            pastaCliente = Path.Combine(caminhoFinal, "Cliente");
            Directory.CreateDirectory(pastaCliente);

            pastaFornecedor = Path.Combine(caminhoFinal, "Fornecedor");
            Directory.CreateDirectory(pastaFornecedor);

            pastaMateriaPrima = Path.Combine(caminhoFinal, "MateriaPrima");
            Directory.CreateDirectory(pastaMateriaPrima);

            pastaProduto = Path.Combine(caminhoFinal, "Produto");
            Directory.CreateDirectory(pastaProduto);

            pastaRisco = Path.Combine(caminhoFinal, "Risco");
            Directory.CreateDirectory(pastaRisco);

            pastaBloqueado = Path.Combine(caminhoFinal, "Bloqueado");
            Directory.CreateDirectory(pastaBloqueado);

            pastaVenda = Path.Combine(caminhoFinal, "Venda");
            Directory.CreateDirectory(pastaVenda);

        }

        public void GravarProduto(Produto produto)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(caminhoFinal, pastaProduto)))
            {
                sw.WriteLine(produto.ToString());
            }
        }
    }
}

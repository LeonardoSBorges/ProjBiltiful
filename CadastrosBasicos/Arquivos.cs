using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos
{
    public class Arquivos
    {

        public static void GerarPastas()
        {
            string caminhoInicial = Directory.GetCurrentDirectory();
            string caminhoFinal = Path.Combine(caminhoInicial, "ProjBiltiful");
            Directory.CreateDirectory(caminhoFinal);

            string pastaCliente = Path.Combine(caminhoFinal, "Cliente");
            Directory.CreateDirectory(pastaCliente);

            string pastaFornecedor = Path.Combine(caminhoFinal, "Fornecedor");
            Directory.CreateDirectory(pastaFornecedor);

            string pastaMateriaPrima = Path.Combine(caminhoFinal, "MateriaPrima");
            Directory.CreateDirectory(pastaMateriaPrima);

            string pastaProduto = Path.Combine(caminhoFinal, "Produto");
            Directory.CreateDirectory(pastaProduto);

            string pastaRisco = Path.Combine(caminhoFinal, "Risco");
            Directory.CreateDirectory(pastaRisco);

            string pastaBloqueado = Path.Combine(caminhoFinal, "Bloqueado");
            Directory.CreateDirectory(pastaBloqueado);

            string pastaVenda = Path.Combine(caminhoFinal, "Venda");
            Directory.CreateDirectory(pastaVenda);

        }

        public void GravarProduto(Produto produto)
        {
            //StreamWriter sw = new StreamWriter()
        }
    }
}

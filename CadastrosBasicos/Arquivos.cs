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
            Console.WriteLine(caminhoInicial);
            caminhoFinal = Path.Combine(caminhoInicial, "ProjBiltiful");
            Directory.CreateDirectory(caminhoFinal);

            pastaMateriaPrima = Path.Combine(caminhoFinal, "MateriaPrima");
            Directory.CreateDirectory(pastaMateriaPrima);

            pastaProduto = Path.Combine(caminhoFinal, "Produto");
            Directory.CreateDirectory(pastaProduto);

        }
        
        public void GravarProduto(Produto produto)
        {
            
            using (StreamWriter sw = new StreamWriter(Path.Combine(caminhoFinal, pastaProduto) + "Cosmetico.dat"))
            {
                sw.WriteLine(produto.ToString());
            }
        }
    }
}

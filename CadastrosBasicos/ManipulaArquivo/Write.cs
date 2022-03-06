using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos.ManipulaArquivos
{
    public class Write
    {
        public string caminhoFinal { get; set; }
        public string pastaCliente { get; set; }
        public string pastaFornecedor { get; set; }
        public string pastaMateriaPrima { get; set; }
        public string pastaProduto { get; set; }
        public string pastaRisco { get; set; }
        public string pastaBloqueado { get; set; }
        public string pastaVenda { get; set; }

        public Write()
        {
            GerarPastas();
        }

        public void GerarPastas()
        {
            string caminhoInicial = Directory.GetCurrentDirectory();
            Console.WriteLine(caminhoInicial);
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
        public void GravarNovoCliente(Cliente cliente)
        {
            try
            {
                string total = $"{cliente.cpf}{cliente.nome}{cliente.dnascimento.ToString("dd/MM/yyyy")}{cliente.sexo}{cliente.ucompra.ToString("dd/MM/yyyy")}{cliente.dcadastro.ToString("dd/MM/yyyy")}{cliente.situacao}";

                string local = pastaCliente + "\\Cliente.dat";
                if (!File.Exists(local))
                {
                    using (StreamWriter sw = new StreamWriter(local))
                    {
                        Console.WriteLine("Arquivo criado com sucesso!\nCliente inserido com sucesso");
                        sw.WriteLine(total);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(local, append: true))
                    {
                        sw.WriteLine(total);
                        Console.WriteLine("Cliente inserido com sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }
        
        public void GravarNovoFornecedor(Fornecedor fornecedor)
        {
            try
            {
                string total = $"{fornecedor.cnpj}{fornecedor.rsocial}{fornecedor.dabertudora.ToString("dd/MM/yyyy")}{fornecedor.ucompra.ToString("dd/MM/yyyy")}{fornecedor.dcadastro.ToString("dd/MM/yyyy")}{fornecedor.situacao}";

                string local = caminhoFinal + "\\Fornecedor.dat";
                if (!File.Exists(local))
                {
                    using (StreamWriter sw = new StreamWriter(local))
                    {
                        Console.WriteLine("Arquivo criado com sucesso!\nCliente inserido com sucesso");
                        sw.WriteLine(total);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(local, append: true))
                    {
                        sw.WriteLine(total);
                        Console.WriteLine("Cliente inserido com sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }
        public void GravarProduto(Produto produto)
        {
            //using (StreamWriter sw = new StreamWriter())
        }
    }
}

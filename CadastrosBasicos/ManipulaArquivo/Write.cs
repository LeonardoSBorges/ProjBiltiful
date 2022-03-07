using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastrosBasicos.ManipulaArquivo;

namespace CadastrosBasicos.ManipulaArquivos
{
    public class Write
    {
        public string CaminhoFinal { get; set; }
        public string CaminhoCadastro { get; set; }
        public string ClienteInadimplente { get; set; }
        public string CadatroFornecedor { get; set; }
        public Write()
        {
            AcharArquivos();
        }

        public void AcharArquivos()
        {
            string caminhoInicial = Directory.GetCurrentDirectory();
            CaminhoFinal = Path.Combine(caminhoInicial, "DataBase");
            if (!File.Exists(CaminhoFinal))
            {
                Directory.CreateDirectory(CaminhoFinal);
            }
            CaminhoCadastro = CaminhoFinal + "\\Cliente.dat";
            if (!File.Exists(CaminhoCadastro))
                File.Create(CaminhoCadastro).Close();
            ClienteInadimplente = CaminhoFinal + "\\ClientesInadimplentes.dat";
            if (!File.Exists(ClienteInadimplente))
                File.Create(ClienteInadimplente).Close();
            CadatroFornecedor = CaminhoFinal + "\\Fornecedor.dat";
            if (!File.Exists(ClienteInadimplente))
                File.Create(ClienteInadimplente).Close();
        }
        public void BloqueiaCliente(string cpf)
        {

            try
            {
                using (StreamWriter sw = new StreamWriter(ClienteInadimplente, append: true))
                {
                    sw.WriteLine(cpf);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }
        //editar cliente
        public void EditarCliente(Cliente clienteAtualizado)
        {

            Read read = new Read();
            List<Cliente> clientes = read.ListaArquivoCliente();
            int posicao = 0;
            try
            {
                while (clientes[posicao] != null)
                {
                    if (clienteAtualizado.CPF == clientes[posicao].CPF)
                    {
                        clientes[posicao] = clienteAtualizado;
                    }
                    posicao++;
                }
                using (StreamWriter sw = new StreamWriter(ClienteInadimplente))
                {
                    posicao = 0;
                    while (clientes[posicao] != null)
                    {
                        sw.WriteLine(clientes[posicao].RetornoArquivo());
                    }
                    Console.WriteLine("Registro atualizado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

        //Gravar novo cliente no arquivo
        public void GravarNovoCliente(Cliente cliente)
        {
            try
            {

                string total = cliente.RetornoArquivo();


                using (StreamWriter sw = new StreamWriter(CaminhoCadastro, append: true))
                {
                    sw.WriteLine(total);
                    Console.WriteLine("Cliente inserido com sucesso");
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
                string total = fornecedor.CNPJ + fornecedor.RSocial + fornecedor.DAbertura.ToString("dd/MM/yyyy") + fornecedor.UCompra.ToString("dd/MM/yyyy") + fornecedor.DCadastro.ToString("dd/MM/yyyy") + fornecedor.Situacao;

                using (StreamWriter sw = new StreamWriter(CaminhoCadastro, append: true))
                {
                    sw.WriteLine(total);
                    Console.WriteLine("Cliente inserido com sucesso");
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

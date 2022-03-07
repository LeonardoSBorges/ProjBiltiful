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
        public string CaminhoFinal { get; set; }
        public string CaminhoCadastro { get; set; }
        public string ClienteInadimplente { get; set; }
        public string CaminhoFornecedor { get; set; }
        public string CaminhoBloqueado { get; set; }

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
            ClienteInadimplente = CaminhoFinal + "\\Risco.dat";
            if (!File.Exists(ClienteInadimplente))
                File.Create(ClienteInadimplente).Close();
            CaminhoBloqueado = CaminhoFinal + "\\Bloqueado.dat";
            if (!File.Exists(CaminhoBloqueado))
                File.Create(CaminhoBloqueado).Close();
            CaminhoFornecedor = CaminhoFinal + "\\Fornecedor.dat";
            if (!File.Exists(CaminhoFornecedor))
                File.Create(CaminhoFornecedor).Close();
        }
        public void BloquearFornecedor(string cnpj)
        {

            try
            {
                using (StreamWriter sw = new StreamWriter(CaminhoBloqueado, append: true))
                {
                    sw.WriteLine(cnpj);
                    Console.WriteLine("Fornecedor Bloqueado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
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
                        sw.WriteLine(clientes[posicao].RetornaArquivo());
                    }
                    Console.WriteLine("Registro atualizado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }
        public void EditarFornecedor(Fornecedor fornecedorAtualizado)
        {

            Read read = new Read();
            List<Fornecedor> fornecedores = read.ListaArquivoFornecedor();
            int posicao = 0;
            try
            {
                while (fornecedores[posicao] != null)
                {
                    if (fornecedorAtualizado.CNPJ == fornecedores[posicao].CNPJ)
                    {
                        fornecedores[posicao] = fornecedorAtualizado;
                    }
                    posicao++;
                }
                using (StreamWriter sw = new StreamWriter(CaminhoFornecedor))
                {
                    posicao = 0;
                    while (fornecedores[posicao] != null)
                    {
                        sw.WriteLine(fornecedores[posicao].RetornaArquivo());
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

                string total = cliente.RetornaArquivo();


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
                string total = fornecedor.RetornaArquivo();

                using (StreamWriter sw = new StreamWriter(CaminhoFornecedor, append: true))
                {
                    sw.WriteLine(total);
                    Console.WriteLine("Fornecedor inserido com sucesso");
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

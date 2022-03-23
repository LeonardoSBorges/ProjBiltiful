using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CadastrosBasicos.ManipulaArquivos
{
    public class Read
    {
        public string CaminhoFinal { get; set; }
        public string CaminhoCadastro { get; set; }
        public string ClienteInadimplente { get; set; }
        public string CaminhoFornecedor { get; set; }
        public string CaminhoBloqueado { get; set; }

        public Read()
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
        public bool ProcurarCNPJBloqueado(string cnpj)
        {

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            string cnpjBloqueado = "";

            try
            {
                using (StreamReader sr = new StreamReader(CaminhoBloqueado))
                {
                    cnpjBloqueado = sr.ReadLine();

                    while (cnpjBloqueado != null)
                    {
                        if (cnpjBloqueado == cnpj)
                        {

                            return true;
                        }
                        cnpjBloqueado = sr.ReadLine();
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }

            return false;

        }
        public bool ProcurarCPFBloqueado(string cpf)
        {

            cpf = cpf.Replace(".", "").Replace("-", "");
            string cpfBloqueado = "";
          
            try
            {
                using (StreamReader sr = new StreamReader(ClienteInadimplente))
                {
                    cpfBloqueado = sr.ReadLine();

                    while (cpfBloqueado != null)
                    {
                        if (cpfBloqueado == cpf)
                        {
                            return true;
                        }
                        cpfBloqueado = sr.ReadLine();
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }

            return false;

        }
        public bool VerificaListaFornecedor()
        {
            if (File.ReadAllLines(CaminhoFornecedor).Length == 0)
                return false;
            else
                return true;
        }
        public bool VerificaListaCliente()
        {
            if (File.ReadAllLines(CaminhoCadastro).Length == 0)
                return false;
            else
                return true;
        }
        
        public Fornecedor ProcurarFornecedor(string procuraCnpj)
        {
            string procuraFornecedor = "", rSocial = "";
            procuraCnpj = procuraCnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            DateTime dAbertura, uCompra, dCadastro;
            char situacao;
            Fornecedor fornecedor;
            bool bloqueado = true;
            if (!File.Exists(CaminhoFornecedor))
                File.Create(CaminhoFornecedor).Close();
            try
            {

                using (StreamReader sr = new StreamReader(CaminhoFornecedor))
                {
                    procuraFornecedor = sr.ReadLine();

                    while (procuraFornecedor != null)
                    {
                        string cnpj = procuraFornecedor.Substring(0, 14);
                        if (procuraCnpj == cnpj)
                        {
                            rSocial = procuraFornecedor.Substring(14, 50).Trim();
                            dAbertura = DateTime.Parse(procuraFornecedor.Substring(64, 10));
                            uCompra = DateTime.Parse(procuraFornecedor.Substring(74, 10));
                            dCadastro = DateTime.Parse(procuraFornecedor.Substring(84, 10));
                            situacao = char.Parse(procuraFornecedor.Substring(94, 1));
                            fornecedor = new Fornecedor(cnpj, rSocial, dAbertura, uCompra, dCadastro, situacao, bloqueado);
                            return fornecedor;
                        }

                        procuraFornecedor = sr.ReadLine();
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            return null;
        }


        public Cliente ProcuraCliente(string cpf)
        {
            string procuraCliente = "";
            Cliente cliente;
            cpf = cpf.Replace(".", "").Replace("-", "");
            bool risco = true;
            try
            {

                using (StreamReader sr = new StreamReader(CaminhoCadastro))
                {
                    procuraCliente = sr.ReadLine();

                    while (procuraCliente != null)
                    {
                        string recebeCpf = procuraCliente.Substring(0, 11);
                        if (recebeCpf == cpf)
                        {
                            string nome = procuraCliente.Substring(11, 50);
                            DateTime dNascimento = DateTime.Parse(procuraCliente.Substring(61, 10));
                            char sexo = char.Parse(procuraCliente.Substring(71, 1));
                            DateTime uCompra = DateTime.Parse(procuraCliente.Substring(72, 10));
                            DateTime dCadastro = DateTime.Parse(procuraCliente.Substring(82, 10));
                            char situacao = char.Parse(procuraCliente.Substring(92, 1));
                            cliente = new Cliente(cpf, nome, dNascimento, sexo, uCompra, dCadastro, situacao,risco);
                            return cliente;
                        }
                        procuraCliente = sr.ReadLine();
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            return null;
        }

    }
}

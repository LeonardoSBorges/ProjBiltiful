using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos.ManipulaArquivo
{
    public class Read
    {
        public string caminhoFinal { get; set; }
        public string pastaCliente { get; set; }
        public string pastaFornecedor { get; set; }
        public string pastaMateriaPrima { get; set; }
        public string pastaProduto { get; set; }
        public string pastaRisco { get; set; }
        public string pastaBloqueado { get; set; }
        public string pastaVenda { get; set; }

        public Read()
        {
            AcharArquivos();
        }

        public void AcharArquivos()
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


        public Fornecedor ProcurarFornecedor(string procuraCnpj)
        {
            string procuraFornecedor = "";
            string file = pastaFornecedor + "\\Fornecedor.dat";
            Fornecedor fornecedor;
            try
            {
                if (File.Exists(file))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        procuraFornecedor = sr.ReadLine();

                        while (procuraFornecedor != null)
                        {
                            string cnpj = procuraFornecedor.Substring(0, 14);
                            if (procuraCnpj == cnpj)
                            {
                                string rSocial = procuraCnpj.Substring(14, 50);
                                DateTime dAbertura = DateTime.Parse(procuraFornecedor.Substring(64, 10));
                                DateTime uCompra = DateTime.Parse(procuraFornecedor.Substring(74, 10));
                                DateTime dCadastro = DateTime.Parse(procuraFornecedor.Substring(84, 10));
                                char situacao = char.Parse(procuraFornecedor.Substring(94, 1));
                                return new Fornecedor(cnpj, rSocial, dAbertura, uCompra, dCadastro, situacao); ;
                            }


                        }
                        return null;
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            return null;
        }


        public Cliente ProcuraCliente(string cpf)
        {
            string procuraCliente = "";
            string file = pastaCliente + "\\Cliente.dat";
            Cliente cliente;
            try
            {
                if (File.Exists(file))
                {
                    using (StreamReader sr = new StreamReader(file))
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
                                cliente = new Cliente(cpf, nome, dNascimento, sexo, uCompra, dCadastro, situacao);
                                Console.WriteLine("Cliente ja cadastrado");
                                Console.WriteLine(cliente.ToString());
                                Console.ReadKey();
                                return cliente;
                            }

                            procuraCliente = sr.ReadLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Nada foi encontrado!");

                    return null;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            return null;
        }
        //public Cliente LerArquivoCliente()
        //{
        //    Cliente cliente = new Cliente();
        //    try
        //    {
        //        using (StreamReader sr = new StreamReader(pastaCliente))
        //        {
        //            string value = sr.ReadLine();
        //            bool flag = false;
        //            string cpf = value.Substring(0, 11);
        //            Console.WriteLine(cpf);
        //            do
        //            {
        //                if (cpf == cliente.CPF)
        //                {
        //                    cliente = cpf;
        //                }
        //            }while (flag != true);
        //        }

        //        return cliente;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Ocorreu um erro: " + ex.Message);
        //    }
        //}
    }
}

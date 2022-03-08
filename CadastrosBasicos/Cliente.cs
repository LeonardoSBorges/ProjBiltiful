using CadastrosBasicos.ManipulaArquivos;
using System;
using System.Collections.Generic;

namespace CadastrosBasicos
{
    public class Cliente
    {
        public Write write = new Write();
        public Read read = new Read();
        public string CPF { get; private set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public Cliente()
        {

        }

        public Cliente(string cpf, string name, DateTime dataNascimento, char sexo, char situacao)
        {
            CPF = cpf;
            Nome = name;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            UltimaVenda = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public Cliente(string cpf, string name, DateTime dataNascimento, char sexo, DateTime UltimaCompra, DateTime dataCadastro, char situacao)
        {
            CPF = cpf;
            Nome = name;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            UltimaVenda = UltimaCompra;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }


        public void BloqueiaCadastro()
        {
            Cliente cliente;
            Console.WriteLine("Insira o CPF para bloqueio: ");
            string cpf = Console.ReadLine();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (read.ProcurarCPFBloqueado(cpf))
            {
                Console.WriteLine("Ja esta bloqueado");
            }
            else
            {
                if (Validacoes.ValidarCpf(cpf))
                {
                    cliente = read.ProcuraCliente(cpf);
                    if (cliente != null)
                    {
                        write.BloqueiaCliente(cliente.CPF);
                        Console.WriteLine("CPF bloqueado!");
                    }
                }
                else
                    Console.WriteLine("CPF incorreto!");
            }
        }
        public Cliente Editar()
        {
            Cliente cliente;
            Console.WriteLine("Somente algumas informacoes podem ser alterada como (Nome/Data de Nascimento/sexo/Situacao), caso nao queira alterar alguma informacao pressione enter!");
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            cliente = read.ProcuraCliente(cpf);
            if (cliente != null)
            {
                Console.WriteLine("Nome: ");
                string nome = Console.ReadLine().Trim().PadLeft(50, ' ');
                Console.WriteLine("Data de nascimento: ");
                bool flag = DateTime.TryParse(Console.ReadLine(), out DateTime dNascimento);
                Console.WriteLine("Situacao: ");
                bool flagSituacao = char.TryParse(Console.ReadLine(), out char situacao);

                cliente.Nome = nome == "" ? cliente.Nome : nome;
                cliente.DataNascimento = flag == false ? cliente.DataCadastro : dNascimento;
                cliente.Situacao = flagSituacao == false ? cliente.Situacao : situacao;

                write.EditarCliente(cliente);
            }
            return cliente;
        }
        public string RetornaArquivo()
        {
            return $"{CPF}{Nome}{DataNascimento.ToString("dd/MM/yyyy")}{Sexo}{UltimaVenda.ToString("dd/MM/yyyy")}{DataCadastro.ToString("dd/MM/yyyy")}{Situacao}";

        }
        public void Navegar()
        {
            Console.WriteLine("============== Clientes ==============");
            List<Cliente> lista = read.ListaArquivoCliente();
            int opcao = 0,posicao = 0;
            bool flag = false;
            do
            {


                if (opcao == 0)
                {
                    Console.WriteLine(lista[posicao].ToString());
                }
                else if (opcao == 1)
                {
                    posicao++;
                    Console.WriteLine(lista[posicao].ToString());
                }
                else if (opcao == 2)
                {
                    posicao--;
                    Console.WriteLine(lista[posicao].ToString());
                }
                else if (opcao == 3)
                {
                    posicao = 0;
                    Console.WriteLine(lista[posicao].ToString());
                }
                else if (opcao == 4)
                {
                    posicao = lista.Count;
                    Console.WriteLine(lista[posicao].ToString());
                }


                Console.WriteLine(@"1. Proximo 
2. Anterior
3. Primeiro
4. Ultimo

");
                do
                {
                    flag = int.TryParse(Console.ReadLine(), out opcao);
                } while (flag != true);

            } while (opcao != 0);
        }
        public override string ToString()
        {
            return $"CPF: {CPF}\nNome: {Nome.Trim()}\nData de nascimento: {DataNascimento.ToString("dd/MM/yyyy")}\nSexo: {Sexo}\nUltima Compra: {UltimaVenda.ToString("dd/MM/yyyy")}\nDia de Cadastro: {DataCadastro.ToString("dd/MM/yyyy")}\nSituacao: {Situacao}";
        }

    }
}

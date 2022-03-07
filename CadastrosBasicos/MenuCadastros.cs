using CadastrosBasicos.ManipulaArquivo;
using CadastrosBasicos.ManipulaArquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos
{
    
    public class MenuCadastros
    {
        public static Write write = new Write();
        public static Read read = new Read();
        public static void SubMenu()
        {
            Cliente cliente;
            Fornecedor fornecedor;
           //Risco risco = new Risco();
            

            //Este menu sera utilizado para testes
            int value = -1;
            while (value != 0)
            {
                Console.Write(@"1. Cadastrar cliente
2. Editar registro de cliente
3. Bloquear cliente
4. Cadastrar fornecedor
5. Editar registro de fornecedor
6. Cadastrar materia prima
7. Cadastrar produtos
8. Cadastro de Inadimplentes
9. Cadastro de Fornecedores Bloqueados
0 - Voltar ao menu anterior
Insira uma opcao valida: 
");
                value = int.Parse(Console.ReadLine());

                bool flag = false;

                switch (value)
                {
                    case 0:
                        // sair
                        break;
                    case 1:
                        //Cadastrar
                        DateTime dNascimento;
                        Console.Write("Data de nascimento: ");
                        do
                        {
                            flag = DateTime.TryParse(Console.ReadLine(), out dNascimento);
                        } while (flag != true);
                        if (Validacoes.CalculaData(dNascimento))
                        {
                            cliente = RegistrarCliente(dNascimento);
                        }
                        break;
                    case 2:
                        Cliente cliente2 = new Cliente();
                        cliente2.Editar();
                        break;
                    case 3:
                        Cliente cliente3 = new Cliente();
                        cliente3.BloqueiaCadastro();
                        break;
                    case 4:
                        DateTime dCriacao;
                        Console.Write("Data de criacao da empresa:");
                        do
                        {
                            flag = DateTime.TryParse(Console.ReadLine(), out dCriacao);
                        } while (flag != true);
                        if (Validacoes.CalculaCriacao(dCriacao))
                        {
                            fornecedor = RegistrarFornecedor(dCriacao);
                            write.GravarNovoFornecedor(fornecedor);
                        }
                        break;
                    case 5:
                        break;
                    case 6:
                       
                    case 7:
                        break;

                }

                Console.ReadKey();
                Console.Clear();
            }

        }
        public static Fornecedor RegistrarFornecedor(DateTime dFundacao)
        {
            string rSocial = "", cnpj = "";
            Read read = new Read();
            char situacao;
            do
            {
                Console.Write("CNPJ: ");
                cnpj = Console.ReadLine();
                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            } while (Validacoes.ValidarCnpj(cnpj) == false);
            Fornecedor f = read.ProcurarFornecedor(cnpj);
            if (f == null)
            {
                Console.Write("Razao social: ");
                rSocial = Console.ReadLine().Trim().PadLeft(50, ' ');
                Console.Write("Situacao (A - Ativo/ I - Inativo): ");
                situacao = char.Parse(Console.ReadLine());
            }
            else
                return f;

            return new Fornecedor(cnpj, rSocial, dFundacao, situacao);

        }
        public static Cliente RegistrarCliente(DateTime dNascimento)
        {
            string cpf = "", nome = "";
            Read read = new Read();
            char situacao, sexo;
            do
            {
                Console.Write("CPF: ");
                cpf = Console.ReadLine();
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");

            } while (Validacoes.ValidarCpf(cpf) == false);
            Cliente c = read.ProcuraCliente(cpf);

            if (c == null)
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine().Trim().PadLeft(50, ' ');
                Console.Write("Genero (M - Masculino/ F - Feminino): ");
                sexo = char.Parse(Console.ReadLine());
                Console.Write("Situacao (A - Ativo/ I - Inativo): ");
                situacao = char.Parse(Console.ReadLine());
                write.GravarNovoCliente(new Cliente(cpf, nome, dNascimento, sexo, situacao));
            }
            else
            {
                Console.WriteLine("Cliente ja cadastrado!!");
                return c;
            }
            return null;
        }
        public void EscreverArquivo(Cliente cliente)
        {
            Write write = new Write();

            write.GravarNovoCliente(cliente);

        }
    }
}

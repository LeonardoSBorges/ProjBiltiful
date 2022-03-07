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

        public static void SubMenu()
        {
            Cliente cliente;
            Fornecedor fornecedor;
            Risco risco;
            Read read = new Read();
            Write write = new Write();
            //Este menu sera utilizado para testes
            int value = -1;
            while (value != 0)
            {
                Console.Write(@"1. Cadastrar cliente
2. Cadastrar fornecedor
3. Cadastrar materia prima
4. Cadastrar produtos
5. Cadastro de Inadimplentes
6. Cadastro de Fornecedores Bloqueados
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
                            write.GravarNovoCliente(cliente);
                        }
                        break;
                    case 2:
                        DateTime dCriacao;
                        Console.Write("Data de criacao da empresa:");
                        do { 
                        flag = DateTime.TryParse(Console.ReadLine(), out dCriacao);
                        } while (flag != true);
                        if (Validacoes.CalculaCriacao(dCriacao)) 
                        {
                            fornecedor = RegistrarFornecedor(dCriacao);
                            write.GravarNovoFornecedor(fornecedor);
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        Console.Write("CPF: ");
                        string cpf = Console.ReadLine();
                        risco = new Risco(cpf);
                        cliente = read.ProcuraCliente(cpf);
                        if(cliente == null)
                            Console.WriteLine("Nenhum cadastro encontrado");
                        break;
                    case 6:
                        break;

                }

                Console.ReadKey();
                Console.Clear();
            }

        }
        public static Fornecedor RegistrarFornecedor(DateTime dFundacao)
        {
            string rSocial = "",cnpj = "";
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
            if (f == null) {
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
            }
            else
                return c;

            return new Cliente(cpf, nome, dNascimento, sexo, situacao);
        }
        public void EscreverArquivo(Cliente cliente)
        {
            Write write = new Write();

            write.GravarNovoCliente(cliente);

        }
    }
}

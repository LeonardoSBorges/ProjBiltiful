using CadastrosBasicos.ManipulaArquivos;
using System;

namespace CadastrosBasicos
{
    public class Fornecedor
    {
        public Write write = new Write();
        public Read read = new Read();
        public string CNPJ { get; set; }
        public string RSocial { get; set; }
        public DateTime DAbertura { get; set; }
        public DateTime UCompra { get; set; }
        public DateTime DCadastro { get; set; }
        public char Situacao { get; set; }

        public Fornecedor()
        {

        }
        public Fornecedor(string cnpj, string rSocial, DateTime dAbertura,char situacao)
        {
            CNPJ = cnpj;
            RSocial = rSocial;
            DAbertura = dAbertura;
            UCompra = DateTime.Now;
            DCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public Fornecedor(string cnpj, string rSocial, DateTime dAbertura, DateTime uCompra, DateTime dCadastro, char situacao)
        {
            CNPJ = cnpj;
            RSocial = rSocial;
            DAbertura = dAbertura;
            UCompra = DateTime.Now;
            DCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public void BloqueiaFornecedor()
        {
            Fornecedor fornecedor;
            Console.WriteLine("Insira o CNPJ para bloqueio: ");
            string cnpj = Console.ReadLine();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (read.ProcurarCNPJBloqueado(cnpj))
            {
                Console.WriteLine("Ja esta bloqueado");
            }
            else
            {
                if (Validacoes.ValidarCnpj(cnpj))
                {
                    fornecedor = read.ProcurarFornecedor(cnpj);
                    if (fornecedor != null)
                    {
                        write.BloquearFornecedor(fornecedor.CNPJ);
                        Console.WriteLine("CNPJ bloqueado!");
                    }
                }
                else
                    Console.WriteLine("CNPJ incorreto!");
            }
        }
        public string RetornaArquivo()
        {
            return CNPJ + RSocial + DAbertura.ToString("dd/MM/yyyy") + UCompra.ToString("dd/MM/yyyy") + DCadastro.ToString("dd/MM/yyyy") + Situacao;
        }
        public Fornecedor Editar()
        {
            Fornecedor fornecedor;
            Console.WriteLine("Somente algumas informacoes podem ser alterada como (Razao social/situacao), caso nao queira alterar alguma informacao pressione enter!");
            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine();

            fornecedor = read.ProcurarFornecedor(cnpj);
            if (fornecedor != null)
            {
                Console.WriteLine("Razao social: ");
                string nome = Console.ReadLine().Trim().PadLeft(50, ' ');
                Console.WriteLine("Situacao: ");
                bool flagSituacao = char.TryParse(Console.ReadLine(), out char situacao);

                fornecedor.RSocial = nome == "" ? fornecedor.RSocial : nome;

                fornecedor.Situacao = flagSituacao == false ? fornecedor.Situacao : situacao;

                write.EditarFornecedor(fornecedor);
            }
            return fornecedor;
        }
        public override string ToString()
        {
            return $"CNPJ: {CNPJ}\nRSocial: {RSocial.Trim()}\nData de Abertura da empresa: {DAbertura.ToString("dd/MM/yyyy")}\nUltima Compra: {UCompra.ToString("dd/MM/yyyy")}\nData de Cadastro: {DCadastro.ToString("dd/MM/yyyy")}\nSituacao: {Situacao}";
        }
    }
}

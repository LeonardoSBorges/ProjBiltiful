using CadastrosBasicos.ManipulaArquivo;
using CadastrosBasicos.ManipulaArquivos;
using System;

namespace CadastrosBasicos
{
    public class Cliente
    {
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

        public Cliente Editar(Cliente cliente)
        {
            Console.WriteLine("Somente algumas informacoes podem ser alterada como (Nome/Data de Nascimento/sexo/Situacao), caso nao queira alterar alguma informacao pressione enter!");

            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine().Trim().PadLeft(50, ' ');
            Console.WriteLine("Data de nascimento: ");
            bool flag= DateTime.TryParse(Console.ReadLine(), out DateTime dNascimento);
            Console.WriteLine("Situacao: ");
            bool flagSituacao = char.TryParse(Console.ReadLine(), out char situacao); 
            
            cliente.Nome = nome == "" ? cliente.Nome : nome;
            cliente.DataNascimento = flag == false ? cliente.DataCadastro : dNascimento;
            cliente.Situacao = flagSituacao == false ? cliente.Situacao : situacao;

            return cliente;
        }
        public override string ToString()
        {
            return $"CPF: {CPF}\nNome: {Nome.Trim()}\nData de nascimento: {DataNascimento.ToString("dd/MM/yyyy")}\nSexo: {Sexo}\nUltima Compra: {UltimaVenda.ToString("dd/MM/yyyy")}\nDia de Cadastro: {DataCadastro.ToString("dd/MM/yyyy")}\nSituacao: {Situacao}";
        }

    }
}

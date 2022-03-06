using CadastrosBasicos.ManipulaArquivo;
using CadastrosBasicos.ManipulaArquivos;
using System;

namespace CadastrosBasicos
{
    public class Cliente
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime UCompra { get; set; }
        public DateTime DCadastro { get; set; }
        public char Situacao { get; set; }

        public Cliente(string cnpj, string rSocial, DateTime dNascimento, char sexo, char situacao)
        {
            CPF = cnpj;
            Nome = rSocial;
            DNascimento = dNascimento;
            Sexo = sexo;
            UCompra = DateTime.Now;
            DCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public Cliente(string cnpj, string rSocial, DateTime dNascimento, char sexo, DateTime uCompra, DateTime dCadastro, char situacao)
        {
            CPF = cnpj;
            Nome = rSocial;
            DNascimento = dNascimento;
            Sexo = sexo;
            UCompra = uCompra;
            DCadastro = dCadastro;
            Situacao = situacao;
        }

        public override string ToString()
        {
            return $"CPF: {CPF}\nNome: {Nome.Trim()}\nData de nascimento: {DNascimento.ToString("dd/MM/yyyy")}\nSexo: {Sexo}\nUltima Compra: {UCompra.ToString("dd/MM/yyyy")}\nDia de Cadastro: {DCadastro.ToString("dd/MM/yyyy")}\nSituacao: {Situacao}";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos
{
    public class Fornecedor
    {
        public string CNPJ { get; set; }
        public string RSocial { get; set; }
        public DateTime DAbertura { get; set; }
        public DateTime UCompra { get; set; }
        public DateTime DCadastro { get; set; }
        public char Situacao { get; set; }


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

        public override string ToString()
        {
            return $"CNPJ: {CNPJ}\nRSocial: {RSocial}\nData de Abertura da empresa: {DAbertura.ToString("dd/MM/yyyy")}\nUltima Compra: {UCompra.ToString("dd/MM/yyyy")}\nData de Cadastro: {DCadastro.ToString("dd/MM/yyyy")}\nSituacao: {Situacao}";
        }
    }
}

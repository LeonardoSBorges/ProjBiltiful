using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos
{
    public class Fornecedor
    {
        //Classe fornecdor
        public string cnpj { get; set; }
        public string rsocial { get; set; }
        public DateTime dabertudora { get; set; }
        public DateTime ucompra { get; set; }
        public DateTime dcadastro { get; set; }
        public char situacao { get; set; }

        public Fornecedor()
        {
            cnpj = CNPJ();
            rsocial = RSocial();
            situacao = Situacao();
            dabertudora = DateTime.Now;
            ucompra = DateTime.Now;
            dcadastro = DateTime.Now;
            
        }
        public string RSocial()
        {
            string nome;
            bool flag = false;
            do
            {
                Console.Write("Razao social: ");
                nome = Console.ReadLine().Trim();
                if (nome.Length <= 50)
                    flag = true;
            } while (flag != true);
            return nome;
        }

        public string CNPJ()
        {
            bool flag = false;
            string value;
            do
            {
                Console.Write("CNPJ: ");
                value = Console.ReadLine().Trim();

                if (value.Length <= 18)
                {
                    if (Validacoes.ValidarCnpj(value))
                        flag = true;
                    else
                    {
                        Console.WriteLine("CNPJ invalido!");
                        continue;
                    }
                }

            } while (flag != true);
            return value;
        }

        public char Situacao()
        {
            char sit;
            bool flag = false;
            do
            {
                Console.Write("Fornecedor (A – Ativo ou I – Inativo): ");
                flag = char.TryParse(Console.ReadLine().ToUpper().Trim(), out sit);
                if (flag != true)
                    Console.WriteLine("Insira um valor correto");

            } while (flag != true);
            return sit;
        }
    }
}

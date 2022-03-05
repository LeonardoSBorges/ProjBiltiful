using System;

namespace CadastrosBasicos
{
    public class Cliente 
    {
        public string cpf { get; set; }
        public string nome { get; set; }
        public DateTime dnascimento { get; set; }
        private char sexo { get; set; }
        private DateTime ucompra { get; set; }
        private DateTime dcadastro { get; set; }
        private char situacao { get; set; }

        public Cliente()
        {
            cpf = CPF();
            nome = Nome();
            dnascimento = DNascimento();
            sexo = Sexo();
            ucompra = DateTime.Now;
            dcadastro = DateTime.Now;
            situacao = Situacao();
        }

        public string CPF()
        {
            bool flag = false;
            string value = " ";
            do
            {
                Console.Write("CPF: ");
                value = Console.ReadLine().Trim();

                if (value.Length <= 14)
                {
                    if (Validacoes.ValidarCpf(value))
                        flag = true;
                    else
                    {
                        Console.WriteLine("CPF invalido!");
                        continue;
                    }
                }
               
            }while (flag != true);
            return value;
        }

        public string Nome()
        {
            string nome;
            bool flag = false;
            do
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine().Trim();
                if (nome.Length <= 50)
                    flag = true;
            } while (flag != true);
            return nome;
        }
        public DateTime DNascimento()
        {
            DateTime nascimento;
            bool flag = false;

            do
            {
                Console.Write("Nascimento (dd/mm/aaaa): ");
                flag = DateTime.TryParse(Console.ReadLine().Trim(), out nascimento);

                if(flag == false)
                    Console.WriteLine("Data incorreta!");
            } while (flag != true);

            bool value = Validacoes.CalcudaData(nascimento);

            if (value == false)
            {
                Console.WriteLine("Nao pode cadastrar pessoas com menos de 18 anos no sistema!");
                return nascimento;
            }
            else
                return nascimento;
            
        }

        public char Sexo()
        {
            Char genero;
            bool flag = false;

            do {
                Console.Write("Sexo (M/F): ");
                flag = char.TryParse(Console.ReadLine().ToUpper().Trim(),out genero);
                if (flag == false)
                    Console.Write("Genero incorreto, tente novamente");

            } while(flag != true);

            return genero;
        }

        public char Situacao()
        {
            char sit;
            bool flag = false;
            do
            {
                Console.Write("usuario (A – Ativo ou I – Inativo): ");
                flag = char.TryParse(Console.ReadLine().ToUpper().Trim(), out sit);
                if (flag != true)
                    Console.WriteLine("Insira um valor correto");
               
            } while (flag != true);
            return sit;
        }

    }
}

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

        public Cliente()
        {
            cpf = CPF();
            //nome = Nome();
            //dnascimento = DNascimento();
            //sexo = 
        }

        public string CPF()
        {
            bool flag = false;
            string value = " ";
            do
            {
                Console.Write("Insira o cpf para cadastro: ");
                value = Console.ReadLine();

                if (Validacoes.IsValid(value))
                    flag = true;
                else
                    continue;
                   
                        
            }while (flag != true);
            return value;
        }
    }
}

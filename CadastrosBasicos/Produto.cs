using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos
{
    public class Produto
    {
        public string cbarras { get; set; }
        public string nome { get; set; }
        public decimal vvenda { get; set; }
        public DateTime uvenda { get; set; }
        public DateTime dcadastro { get; set; }
        public char situacao { get; set; }

        // nome arquivo: Cosmetico.dat

        public Produto()
        {
            Cadastrar();
        }

        private void Cadastrar()
        {
            char sit = 'A';
            string cod, nomeTemp;
            decimal venda = 0;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine("\n Cadastro de Produto\n");
                Console.Write(" Cod. Barras: 789");
                cod = Console.ReadLine();
                Console.Write(" Nome: ");
                nomeTemp = Console.ReadLine();
                Console.Write(" Valor da Venda: ");
                venda = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine(venda);
                Console.ReadKey();
                Console.Write(" Situacao (A / I): ");
                sit = char.Parse(Console.ReadLine().ToUpper());

                if ((cod == null) && (nomeTemp == null) && (venda < 1))
                {
                    Console.WriteLine(" Nenhum campo podera ser vazio.");
                    Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                    Console.ReadKey();
                }
                else
                {
                    cod = "789" + cod;

                    if (cod.Length != 13)
                    {
                        Console.WriteLine(" Codigo invalido. Digite apenas os ultimos 10 numeros.");
                        Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }
                    else if (nomeTemp.Length > 20)
                    {
                        Console.WriteLine(" Nome invalido. Digite apenas 20 caracteres.");
                        Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }
                    else if ((venda < 1) || (venda > 99999))
                    {
                        Console.WriteLine(" Valor invalido. Apenas valores maior que 0 e menor que 999,99.");
                        Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }
                    else if ((sit != 'A') && (sit != 'I'))
                    {
                        Console.WriteLine(" Situacao invalida.");
                        Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                        Console.ReadKey();
                    }
                    else
                    {
                        flag = false;

                        cbarras = cod;
                        nome = nomeTemp;
                        vvenda = venda;
                        //uvenda = ;
                        dcadastro = DateTime.Now.Date;
                        Console.WriteLine(" Cadastro do Materia-prima concluido com sucesso!");
                        Console.WriteLine(" PRessione ENTER para voltar ao menu");
                        Console.ReadKey();
                    }
                }

            } while (flag);
        }

    }
}


/*decimal.TryParse(Console.ReadLine().ToString(CultureInfo.InvariantCulture), out decimal value1);
                bool flagValue = decimal.TryParse(Console.ReadLine().ToString(CultureInfo.InvariantCulture), out decimal value);
                if (!flagValue)
                {
                    venda = 0;
                }
                else
                {
                    venda = value;
                }


                //venda = Console.ReadLine() == "" ? 0 : Convert.ToDecimal(Console.ReadLine().ToString(CultureInfo.InvariantCulture));*/

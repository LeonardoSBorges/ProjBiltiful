using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos
{
    public class MPrima
    {
        public string id { get; set; }
        public string nome { get; set; }
        public DateTime ucompra { get; set; }
        public DateTime dcadastro { get; set; }
        public char situacao { get; set; }

        // nome arquivo: Materia.dat

        public MPrima()
        {
            Cadastrar();
        }

        public void Cadastrar()
        {
            char sit = 'A';
            string nomeTemp;
            bool flag = true;

            id = ""; // puxar do arquivo

            do
            {
                Console.Clear();
                Console.WriteLine("\n Cadastro de Materia-prima\n");
                Console.Write(" ID: " + id);
                
                Console.Write(" Nome: ");
                nomeTemp = Console.ReadLine();
                Console.Write(" Situacao (A / I): ");
                sit = char.Parse(Console.ReadLine().ToUpper());

                if (nomeTemp == null)
                {
                    Console.WriteLine(" Nenhum campo podera ser vazio.");
                    Console.WriteLine(" Pressione ENTER para voltar ao cadastro.");
                    Console.ReadKey();
                }
                else
                {
                    if (nomeTemp.Length > 20)
                    {
                        Console.WriteLine(" Nome invalido. Digite apenas 20 caracteres.");
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

                        nome = nomeTemp;
                        //ucompra = ;
                        dcadastro = DateTime.Now.Date;
                        Console.WriteLine(" Cadastro do Produto concluido com sucesso!");
                        Console.WriteLine(" PRessione ENTER para voltar ao menu");
                        Console.ReadKey();
                    }
                }

            } while (flag);
        }

        //public MPrima Localizar()
        //{
        //    string cod;
        //    Console.WriteLine("\n Busca de Materia-prima\n");
        //    Console.Write(" Digite o cod. da materia-prima: ");
        //    cod = Console.ReadLine();
        //}

        public void Imprimir()
        {

        }
    }
}

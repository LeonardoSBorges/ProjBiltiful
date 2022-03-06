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



                switch (value)
                {
                    case 0:
                        // sair
                        break;
                    case 1:
                        //Cadastrar
                        cliente = new Cliente();
                        write.GravarNovoCliente(cliente);
                        break;
                    case 2:
                        new Fornecedor();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;

                }

                Console.ReadKey();
                Console.Clear();
            }

        }
    }
}

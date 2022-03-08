using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrosBasicos
{
    public class SubMenu
    {
        public static void SubMenuCadastros()
        {
            
            //Este menu sera utilizado para testes
            int value = -1;
            while (value != 0)
            {
                bool flag = false;
                Console.Write(@"============ Cadastros ============
1. Cliente e Fornecedor
2. Cadastrar materia prima
3. Cadatrar Produtos
0. Voltar ao menu anterior
Insira uma opcao valida: 
");
                do
                {
                    flag = int.TryParse(Console.ReadLine(), out value);
                } while (flag != true);
                switch (value)
                {
                    case 0:
                        break;
                    case 1:
                        MenuCadastros.SubMenu();
                        break;
                    case 2:
                        MPrima menu = new MPrima();
                        menu.Menu();
                        break;
                    case 3:
                        Produto produto = new Produto();
                        produto.Menu();
                        break;
                    default:
                        Console.WriteLine("Opcao escolhida e invalida");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

        }
    }
}

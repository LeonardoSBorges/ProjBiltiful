using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CadastrosBasicos;

namespace ProducaoCosmeticos
{
    public class Producao
    {
        #region Propriedades da Produção
        public string Id { get; set; }
        public string DataProducao { get; set; }
        public string Produto { get; set; }
        public float Quantidade { get; set; }
        public int Contador { get; set; }
        #endregion
        #region Construtor
        public Producao(string id, string dataProducao, string produto, float quantidade)
        {
            Id = id;
            DataProducao = dataProducao;
            Produto = produto;
            Quantidade = quantidade;
            Contador = 1;
        }

        public Producao()
        {

            Contador = 1;

        }

        #endregion


        public void Menu()
        {

            int escolha;

            do
            {

                Console.WriteLine("********** Menu *********");
                Console.WriteLine("\n    Escolha uma opção\n");
                Console.WriteLine("(1) Cadastrar uma produção");
                Console.WriteLine("(2) Localizar um registro");
                Console.WriteLine("(3) Imprimir por registro");
                Console.WriteLine("(4) Sair");
                Console.WriteLine("\n*************************");

                Console.Write("Opção: "); escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {

                    case 1:

                        Console.Clear();
                        Cadastrar();

                        break;

                    case 2:

                        Console.Clear();
                        Localizar();

                        break;

                    case 3:

                        ImprimirPorRegistro();
                        Console.Clear();

                        break;

                    default:

                        break;

                }

            } while (escolha > 0 && escolha < 4);


            Console.ReadKey();

        }


        #region Métodos

        List<Producao> listaProducao = new List<Producao>();
        public void Cadastrar()
        {

            if(Contador == 1)
            {

                LerArquivo();

            }

            string dataProducao = DateTime.Now.ToString("dd/MM/yyyy");
            string produto = null, auxiliarProduto, id;
            float quantidade = 0, auxiliarQuantidade;
            int escolha;
            bool control = false;
            id = Contador.ToString().PadLeft(5, '0');

            if (Contador <= 99999)
            {

                Console.Write("ID: " + id);

                Console.Write("\nData de produção: " + dataProducao);

                Console.Write("\nProduto: \n");


                do
                {

                    Console.Write("Digite o códigos do produto: ");
                    auxiliarProduto = Console.ReadLine();

                    if (BuscarCodigo(auxiliarProduto) == null)
                    {

                        Console.WriteLine("Código de produto inválido!");
                        Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                        Console.ReadKey();
                        Console.Clear();

                    }
                    else
                    {

                        produto = auxiliarProduto;
                        control = true;

                    }

                } while (control != true);

                control = false;

                do
                {

                    Console.Write("\nQuantidade: ");
                    auxiliarQuantidade = float.Parse(Console.ReadLine());


                    if (auxiliarQuantidade > 0 && auxiliarQuantidade < 1000)
                    {

                        quantidade = auxiliarQuantidade;
                        control = true;

                    }
                    else
                    {

                        Console.WriteLine("\nNão é possivel adicionar a quantidade digitada!");
                        Console.WriteLine("\nDigite a quantidade novamente");

                    }

                }
                while (control != true);


                Console.WriteLine("Gostaria de finalizar o registro ou deseja excluí-lo agora mesmo?");
                Console.WriteLine("(1) Finalizar\n(2) Cancelar registro");

                escolha = int.Parse(Console.ReadLine());

                if (escolha == 1)
                {

                    Producao producao = new(id, dataProducao, produto, quantidade);
                    listaProducao.Add(producao);
                    Contador++;
                    string formatado = "" + id + dataProducao.Replace("/", "") + produto + quantidade.ToString("00000");
                    SalvarArquivo(formatado);

                    Console.WriteLine("Registro feito com sucesso!");
                    Console.ReadKey();
                    Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                    Console.Clear();

                }
                else
                {

                    Console.WriteLine("O registro foi cancelado com sucesso!");
                    Console.ReadKey();
                    Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                    Console.Clear();

                }


            }
            else
            {

                Console.WriteLine("Não é possivel registrar mais produções, pois o número limite de 99999 foi atingido!");
                Console.ReadKey();
                Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                Console.Clear();

            }

        }
        public Producao Localizar()
        {

            LerArquivo();

            Producao encontrado;
            string buscaId;

            if (listaProducao.Count == 0)
            {

                Console.WriteLine("Não existe nenhum registro de produção ainda!");
                return null;

            }
            else
            {

                Console.Write("Digite o ID da produção que você deseja localizar: ");

                buscaId = Console.ReadLine();
                encontrado = listaProducao.Find(x => x.Id == buscaId);

                if (encontrado == null)
                {

                    Console.WriteLine("Nenhuma produção com esse id foi localizada!");
                    Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                    Console.ReadKey();
                    Console.Clear();

                    return null;

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(encontrado.ToString());
                    Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                    Console.ReadKey();
                    Console.Clear();

                    return encontrado;

                }
            }
        }

        public void ImprimirPorRegistro()
        {

            LerArquivo();


            int escolha, i = 0;

            if (listaProducao.Count == 0)
            {

                Console.WriteLine("Não existe nenhum registro de produção ainda!");
                Console.ReadKey();
                Console.WriteLine("\n\n\t Pressione ENTER para continuar...");

            }
            else
            {
                Console.Clear();
                Console.WriteLine(listaProducao[i].ToString());

                do
                {
                   

                    Console.WriteLine("\nO que você gostaria de fazer?\n");
                    Console.WriteLine("(1) Ir para o próximo");
                    Console.WriteLine("(2) Ir para o anterior");
                    Console.WriteLine("(3) Ir para o primeiro");
                    Console.WriteLine("(4) Ir para o ultimo");
                    Console.WriteLine("(5) Sair\n");

                    Console.Write("Opção: "); escolha = int.Parse(Console.ReadLine());

                    Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                    Console.ReadKey();
                    Console.Clear();

                    switch (escolha)
                    {
                        case 1:

                            if (i + 1 < listaProducao.Count && listaProducao[i + 1] != null)
                            {
                                
                                Console.WriteLine(listaProducao[i + 1].ToString());
                                i++;

                            }
                            else
                                Console.WriteLine("Não tem um próximo na lista de produção!");

                            break;
                        case 2:

                            if (i > 0 && listaProducao[i - 1] != null)
                            {

                                Console.WriteLine(listaProducao[i - 1].ToString());
                                i--;

                            }
                            else
                                Console.WriteLine("Não tem um anterior na lista de produção!");

                            break;
                        case 3:

                            Console.WriteLine(listaProducao[0].ToString());

                            break;
                        case 4:

                            Console.WriteLine(listaProducao[listaProducao.Count - 1].ToString());

                            break;
                        case 5:

                            break;
                    }
                }
                while (escolha != 5);
            }
        }

        public void SalvarArquivo(string producao)
        {

            string caminhoInicial = Directory.GetCurrentDirectory();

            string caminhoFinal = Path.Combine(caminhoInicial + "\\ProjBiltiful\\");
            Directory.CreateDirectory(caminhoFinal);

            string pastaProducao = Path.Combine(caminhoFinal, "Producao\\");
            Directory.CreateDirectory(pastaProducao);

            string arquivoFinal = Path.Combine(pastaProducao + "Producao.dat");

            if (!File.Exists(arquivoFinal))
            {

                try
                {

                    using (StreamWriter sw = new StreamWriter(arquivoFinal))
                    {

                        sw.WriteLine(producao);

                    }

                }
                catch
                {

                    Console.WriteLine("Algo deu errado...");

                }

            }
            else
            {

                try
                {

                    using (StreamWriter sw = new StreamWriter(arquivoFinal,append:true))
                    {

                        sw.WriteLine(producao);

                    }

                }
                catch
                {

                    Console.WriteLine("Algo deu errado...");

                }

            }

            

        }

        public string BuscarCodigo(string codigo)
        {

            string caminhoInicial = Directory.GetCurrentDirectory();

            string caminhoFinal = Path.Combine(caminhoInicial + "\\ProjBiltiful\\");
            Directory.CreateDirectory(caminhoFinal);

            string pastaProducao = Path.Combine(caminhoFinal, "Producao\\");
            Directory.CreateDirectory(pastaProducao);

            string arquivoFinal = Path.Combine(pastaProducao + "Producao.dat");

            string pastaProduto = Path.Combine(caminhoFinal, "Produto\\");
            Directory.CreateDirectory(pastaProduto);

            string arquivoProduto = Path.Combine(pastaProduto + "Cosmetico.dat");

            string cbarras = null;

            try
            {

                string linha = null;

                using (StreamReader sr = new StreamReader(arquivoProduto))
                {

                    linha = sr.ReadLine();


                    do
                    {

                        if (linha.Substring(0, 13) == codigo)
                        {

                            cbarras = linha.Substring(0, 13);

                        }
                        linha = sr.ReadLine();

                    }
                    while (linha != null);

                }

            }
            catch
            {

                Console.WriteLine("Algo deu errado...");

            }

            return cbarras;

        }

        public void LerArquivo()
        {


            string caminhoInicial = Directory.GetCurrentDirectory();

            string caminhoFinal = Path.Combine(caminhoInicial + "\\ProjBiltiful\\");
            Directory.CreateDirectory(caminhoFinal);

            string pastaProducao = Path.Combine(caminhoFinal, "Producao\\");
            Directory.CreateDirectory(pastaProducao);

            string arquivoFinal = Path.Combine(pastaProducao + "Producao.dat");

            try
            {

                string linha = null;

                using (StreamReader sr = new StreamReader(arquivoFinal))
                {

                    linha = sr.ReadLine();


                    do
                    {

                        Id = linha.Substring(0, 5);
                        DataProducao = linha.Substring(5, 8).Insert(2, "/").Insert(5, "/");
                        Produto = linha.Substring(13, 13);
                        Quantidade = float.Parse(linha.Substring(26, 5));

                        Producao producao = new Producao(Id, DataProducao, Produto, Quantidade);

                        listaProducao.Add(producao);
                        Contador++;


                        linha = sr.ReadLine();

                    }
                    while (linha != null);

                }
            }
            catch
            {

            }
        }


        #endregion
        public override string ToString()
        {
            return 
                "\n******** Registro de Produção ********\n\n"
                +"ID: " + Id.ToString().PadLeft(5, '0')
                + "\nData de produção: " + DataProducao
                + "\nProduto: " + Produto
                + "\nQuantidade: " + Quantidade.ToString("000.#0")
                + "\n\n**************************************";
        }
    }
}

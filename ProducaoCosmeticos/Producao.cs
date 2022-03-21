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

        public void SubMenu()
        {

            string escolha;

            do
            {

                Console.Clear();
                Console.WriteLine("\n=============== PRODUÇÃO ===============");
                Console.WriteLine("1. Cadastrar uma produção");
                Console.WriteLine("2. Localizar um registro");
                Console.WriteLine("3. Imprimir por registro");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("0. Voltar ao menu anterior");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        break;
                    case "1":
                        string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
                        if (File.Exists(caminhoFinal + "\\Cosmetico.dat") && File.Exists(caminhoFinal + "\\Materia.dat"))
                        {
                            Console.Clear();
                            Cadastrar();
                        }
                        else
                        {
                            Console.WriteLine("Não ha produtos ou materias primas cadastradas. Favor verificar!");
                            Console.ReadKey();
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Localizar();
                        break;
                    case "3":
                        ImprimirPorRegistro();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("\n Opção inválida.");
                        Console.WriteLine("\n Pressione ENTER para voltar ao menu.");
                        Console.ReadKey();
                        break;
                }

            } while (escolha != "0");
        }


        #region Métodos

        List<Producao> listaProducao = new List<Producao>();

        ItemProducao itemProducao = new ItemProducao();

        List<ItemProducao> itens = new List<ItemProducao>();

        public void Cadastrar()
        {

            if (Contador == 1)
            {

                LerArquivo();

            }

            string dataProducao = DateTime.Now.ToString("dd/MM/yyyy");
            string produto = null, auxiliarProduto, id, codigoMateriaPrima;
            float quantidade = 0, auxiliarQuantidade, quantidadeMateriaPrima;
            int escolha, opcao = 0;
            bool control = false;


            id = Contador.ToString().PadLeft(5, '0');

            if (Contador <= 99999)
            {

                Console.Write("ID: " + id);

                Console.Write("\nData de produção: " + dataProducao + "\n");


                do
                {

                    Console.Write("Digite o código do produto: ");
                    auxiliarProduto = Console.ReadLine();

                    Produto pproduto = new Produto().RetornaProduto(auxiliarProduto);

                    if (pproduto == null)
                    {

                        Console.WriteLine("Código de produto inválido!");
                        Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (pproduto.Situacao.Equals('I'))
                    {
                        Console.WriteLine("Este produto esta inativo");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        produto = pproduto.CodigoBarras;
                        control = true;
                    }

                } while (control != true);

                control = false;

                do
                {

                    Console.Write("Quantidade: ");
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

                do
                {
                    control = false;

                    MPrima materiaPrima;

                    do
                    {
                        materiaPrima = new MPrima();

                        Console.Write("Digite o código da matéria prima: ");
                        codigoMateriaPrima = Console.ReadLine();

                        materiaPrima = materiaPrima.RetornaMateriaPrima(codigoMateriaPrima);

                        if (materiaPrima == null)
                        {

                            Console.WriteLine("Código de matéria prima inválida!");
                            Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (materiaPrima.Situacao.Equals('I'))
                        {
                            Console.WriteLine("O item escolhido esta inativo.");
                            Console.ReadKey();
                        }
                        else
                        {
                            codigoMateriaPrima = materiaPrima.Id;
                            control = true;
                        }

                    } while (control != true);

                    Console.Write("Digite a quantidade de matéria prima que será utilizada: ");
                    quantidadeMateriaPrima = float.Parse(Console.ReadLine());

                    if (quantidadeMateriaPrima < 0 || quantidadeMateriaPrima >= 1000)
                    {

                        Console.WriteLine("Não é possivel adicionar essa quantidade de matéria prima");
                        continue;

                    }

                    itens.Add(new ItemProducao(id, dataProducao, codigoMateriaPrima, quantidadeMateriaPrima));


                    Console.WriteLine("Gostaria de adicionar mais uma materia prima?\n(1) Sim\n(2) Não");

                    Console.Write("Resposta: "); opcao = int.Parse(Console.ReadLine());



                } while (opcao == 1);

                Console.WriteLine("Gostaria de finalizar o registro ou deseja excluí-lo agora mesmo?");
                Console.WriteLine("1. Finalizar\n2. Cancelar registro");

                Console.Write("Resposta: "); escolha = int.Parse(Console.ReadLine());

                if (escolha == 1)
                {

                    Producao producao = new(id, dataProducao, produto, quantidade);
                    listaProducao.Add(producao);

                    Contador++;

                    string formatado = "" + id + dataProducao.Replace("/", "") + produto + quantidade.ToString("00000");
                    SalvarArquivo(formatado);

                    itemProducao.GravarItemProducao(itens);

                    Console.WriteLine("\n\tRegistro efetuado com sucesso!");
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

            if (Contador == 1)
            {

                LerArquivo();

            }


            Producao encontrado;
            string buscaId;

            if (listaProducao.Count == 0)
            {

                Console.WriteLine("Não existe nenhum registro de produção ainda!");
                Console.ReadKey();
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
                    BuscarItemProducao(encontrado.Id);


                    Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                    Console.ReadKey();
                    Console.Clear();

                    return encontrado;

                }
            }
        }

        public void ImprimirPorRegistro()
        {

            if (Contador == 1)
            {

                LerArquivo();

            }

            int escolha, i = 0;

            if (listaProducao.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Não existe nenhum registro de produção ainda!");
                Console.ReadKey();
                Console.WriteLine("\n\n\t Pressione ENTER para continuar...");

            }
            else
            {
                Console.Clear();
                Console.WriteLine(listaProducao[i].ToString());
                BuscarItemProducao(listaProducao[i].Id);

                do
                {


                    Console.WriteLine("\nO que você gostaria de fazer?\n");
                    Console.WriteLine("1. Ir para o próximo");
                    Console.WriteLine("2. Ir para o anterior");
                    Console.WriteLine("3. Ir para o primeiro");
                    Console.WriteLine("4. Ir para o ultimo");
                    Console.WriteLine("0. Sair\n");

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
                                BuscarItemProducao(listaProducao[i + 1].Id);
                                i++;

                            }
                            else
                                Console.WriteLine("Não tem um próximo na lista de produção!");

                            break;
                        case 2:

                            if (i > 0 && listaProducao[i - 1] != null)
                            {

                                Console.WriteLine(listaProducao[i - 1].ToString());
                                BuscarItemProducao(listaProducao[i - 1].Id);

                                i--;

                            }
                            else
                                Console.WriteLine("Não tem um anterior na lista de produção!");

                            break;
                        case 3:

                            Console.WriteLine(listaProducao[0].ToString());
                            BuscarItemProducao(listaProducao[0].Id);

                            break;
                        case 4:

                            Console.WriteLine(listaProducao[listaProducao.Count - 1].ToString());
                            BuscarItemProducao(listaProducao[listaProducao.Count - 1].Id);

                            break;
                        case 5:

                            break;
                    }
                }
                while (escolha != 0);
            }
        }

        public void SalvarArquivo(string producao)
        {

            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoFinal = Path.Combine(caminhoFinal, "Producao.dat");

            if (!File.Exists(arquivoFinal))
            {

                try
                {

                    using (StreamWriter sw = new StreamWriter(arquivoFinal))
                    {

                        sw.WriteLine(producao);
                        sw.Close();

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

                    using (StreamWriter sw = new StreamWriter(arquivoFinal, append: true))
                    {

                        sw.WriteLine(producao);
                        sw.Close();

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


            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoFinal = Path.Combine(caminhoFinal, "Producao.dat");

            string cbarras = null;

            try
            {

                string linha = null;

                using (StreamReader sr = new StreamReader(arquivoFinal))
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

                    sr.Close();

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

            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoFinal = Path.Combine(caminhoFinal, "Producao.dat");

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

                    sr.Close();

                }
            }
            catch
            {

            }
        }

        public void BuscarItemProducao(string codigoproducao)
        {
            string caminhoInicial = Directory.GetCurrentDirectory();

            string caminhoFinal = Path.Combine(Directory.GetCurrentDirectory(), "DataBase");
            Directory.CreateDirectory(caminhoFinal);

            string arquivoItemProducao = Path.Combine(caminhoFinal, "ItemProducao.dat");

            List<string> listaItemProducao = new();

            try
            {

                string linha = null;

                using (StreamReader sr = new StreamReader(arquivoItemProducao))
                {

                    linha = sr.ReadLine();


                    do
                    {

                        if (codigoproducao == linha.Substring(0, 5))
                        {

                            listaItemProducao.Add(linha.Substring(13, 11));

                        }



                        linha = sr.ReadLine();

                    }
                    while (linha != null);


                    Console.WriteLine("\nLista de matérias primas utilizadas: \n");

                    listaItemProducao.ForEach(item =>
                    {
                        float qntdMatPrima = float.Parse(item.Substring(6, 5));

                        Console.WriteLine("Código da matéria prima: " + item.Substring(0, 6));
                        Console.WriteLine("Quantidade utilizada da matéria prima: " + qntdMatPrima.ToString("000.#0").TrimStart('0') + "\n");

                    });

                    Console.WriteLine("\n\n *********************************************");

                }
            }
            catch
            {

            }

        }

        public override string ToString()
        {
            return
                "\n************ Registro de Produção ************\n\n"
                + "ID: " + Id.ToString().PadLeft(5, '0')
                + "\nData de produção: " + DataProducao
                + "\nProduto: " + Produto
                + "\nQuantidade: " + Quantidade.ToString("000.#0").TrimStart('0');

        }

        #endregion
    }
}

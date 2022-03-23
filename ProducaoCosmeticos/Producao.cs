using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CadastrosBasicos;
using System.Data;
using System.Data.SqlClient;
using ConexaoDB;

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


        ItemProducao itemProducao = new ItemProducao();


        CadastrosBD2 cbd = new CadastrosBD2();

        public void Cadastrar()
        {

            if (Contador == 1)
            {

                List<Producao> listaProducao = cbd.ListaProducao();
                if(listaProducao.Count > 0)
                Contador = listaProducao.Count;

            }

            string dataProducao = DateTime.Now.ToString("dd/MM/yyyy");
            string produto = null, auxiliarProduto, id, codigoMateriaPrima;
            float quantidade = 0, auxiliarQuantidade, quantidadeMateriaPrima;
            int escolha, opcao = 0;
            bool control = false;


            id = Contador.ToString().PadLeft(5, '0');

            if (Contador <= 99999)
            {

                List<ItemProducao> itens = new List<ItemProducao>();

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

                    Console.WriteLine("\nRegistrar Item de produção:\n");

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
                    cbd.RegistraProducaoBD(produto, quantidade);

                    foreach (ItemProducao itemProducao in itens)
                    {

                        string idProducao, codMPrima;
                        float quantidadeMPrima;

                        idProducao = itemProducao.Id;
                        codMPrima = itemProducao.MateriaPrima;
                        quantidadeMPrima = itemProducao.QuantidadeMateriaPrima;

                        cbd.RegistraItemProducaoBD(idProducao, codMPrima, quantidadeMPrima);


                    }

                    Contador++;

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

        public void Localizar()
        {

            bool verifica_tabela = cbd.VerificaTabelaProducao();
            string buscaId;

            if (verifica_tabela == false)
            {

                Console.WriteLine("Não existe nenhum registro de produção ainda!");
                Console.ReadKey();

            }
            else
            {


                Console.Write("Digite o ID da produção que você deseja localizar: ");

                buscaId = Console.ReadLine();

                Producao encontrado = cbd.BuscaProducao(buscaId);

                if (encontrado == null)
                {

                    Console.WriteLine("Nenhuma produção com esse id foi localizada!");
                    Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                    Console.ReadKey();
                    Console.Clear();


                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(encontrado.ToString());
                    BuscarItemProducao(encontrado.Id);


                    Console.WriteLine("\n\n\t Pressione ENTER para continuar...");
                    Console.ReadKey();
                    Console.Clear();


                }
            }
        }

        public void ImprimirPorRegistro()
        {

            int escolha, i = 0;

            List<Producao> listaProducao = cbd.ListaProducao();
            bool verifica_tabela = cbd.VerificaTabelaProducao();

            if (verifica_tabela == true)
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

        public void BuscarItemProducao(string codigoproducao)
        {

            List<ItemProducao> listaItemProducao = cbd.ListaItemProducao();

            Console.WriteLine("\nLista de itens da producao: \n");

            foreach (ItemProducao item in listaItemProducao)
            {
                if (codigoproducao == item.Id)
                {

                    Console.WriteLine("Codigo da materia prima: " + item.MateriaPrima);
                    Console.WriteLine("Quantidade utilizada da matéria prima: " + item.QuantidadeMateriaPrima);

                }
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

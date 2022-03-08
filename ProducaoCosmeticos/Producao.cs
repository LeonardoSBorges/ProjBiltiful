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
        public string produto { get; set; }
        public float Quantidade { get; set; }
        public int Contador { get; set; }
        #endregion
        #region Construtor
        public Producao(string id, string dataProducao, string produto, float quantidade)
        {
            Id = id;
            DataProducao = dataProducao;
            this.produto = produto;
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
                Console.WriteLine("Menu");
                Console.WriteLine("\nEscolha uma opção");
                Console.WriteLine("1- Cadastrar uma produção");
                Console.WriteLine("2- Localizar um registro");
                Console.WriteLine("3- Imprimir por registro");
                Console.WriteLine("4- Sair");

                escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {

                    case 1:

                        Cadastrar();

                        break;

                    case 2:

                        Localizar();

                        break;

                    case 3:

                        ImprimirPorRegistro();

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

            

            string dataProducao = DateTime.Now.ToString("dd/MM/yyyy");
            string produto = "", id;
            float quantidade = 0, auxiliarQuantidade;
            int escolha;
            bool control;
            id = Contador.ToString().PadLeft(5, '0');

            if (Contador <= 99999)
            {

                Console.Write("ID: " + id);

                Console.Write("\nData de produção: " + dataProducao);

                Console.Write("\nProduto: ");

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
                        control = false;

                    }

                }
                while (control != true);


                Console.WriteLine("Gostaria de finalizar o registro ou deseja excluí-lo agora mesmo?");
                Console.WriteLine("1- Finalizar ou 2- Cancelar registro");

                escolha = int.Parse(Console.ReadLine());

                if (escolha == 1)
                {

                    Producao producao = new(id, dataProducao, produto, quantidade);
                    listaProducao.Add(producao);
                    Contador++;
                    string formatado = "" + id + dataProducao.Replace("/", "") + produto + quantidade.ToString("00000");
                    //SalvarArquivo();


                }
                else
                {

                    Console.WriteLine("O registro foi cancelado com sucesso!");

                }


            }
            else
            {

                Console.WriteLine("Não é possivel registrar mais produções, pois o número limite de 99999 foi atingido!");

            }
        }
        public Producao Localizar()
        {
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
                    return null;

                }
                else
                {

                    Console.WriteLine(encontrado.ToString());
                    return encontrado;

                }
            }
        }
        //public void Excluir()
        //{
        //    int confirmacaoDeExclusao;
        //    Producao encontrado = Localizar();

        //    if (encontrado == null)
        //    {

        //        Console.WriteLine("Portanto, não é possível realizar a exclusão!");

        //    }
        //    else
        //    {

        //        Console.WriteLine("Deseja excluir este registro? 1- Sim ou 2- Não");

        //        confirmacaoDeExclusao = int.Parse(Console.ReadLine());

        //        if (confirmacaoDeExclusao == 1)
        //        {

        //            listaProducao.Remove(encontrado);
        //            Console.WriteLine("Produção removida com sucesso!");

        //        }
        //        else
        //        {

        //            Console.WriteLine("Pressione [ENTER] para voltar ao menu");
        //            Console.ReadKey();

        //        }
        //    }
        //}
        public void ImprimirPorRegistro()
        {

            int escolha, i = 0;

            if (listaProducao.Count == 0)
            {

                Console.WriteLine("Não existe nenhum registro de produção ainda!");

            }
            else
            {
                Console.WriteLine(listaProducao[i].ToString());

                do
                {

                    Console.WriteLine("O que você gostaria de fazer?");
                    Console.WriteLine("1- Ir para o próximo");
                    Console.WriteLine("2- Ir para o anterior");
                    Console.WriteLine("3- Ir para o primeiro");
                    Console.WriteLine("4- Ir para o ultimo");
                    Console.WriteLine("5- Sair");

                    escolha = int.Parse(Console.ReadLine());

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

                            Console.WriteLine(listaProducao[listaProducao.Count].ToString());

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


            try
            {

                using(StreamWriter sw = new StreamWriter(arquivoFinal))
                {

                    

                }

            }
            catch
            {


            }

        }

        public void LerArquivo(string arquivo)
        {

            string caminhoInicial = Directory.GetCurrentDirectory();

            string caminhoFinal = Path.Combine(caminhoInicial + "\\ProjBiltiful\\");
            Directory.CreateDirectory(caminhoFinal);

            string pastaProducao = Path.Combine(caminhoFinal, "Producao\\");
            Directory.CreateDirectory(pastaProducao);

            string arquivoFinal = Path.Combine(pastaProducao + "Producao.dat");

            if(arquivo == "produto")
            {

                string pastaProduto= Path.Combine(caminhoFinal, "Produto\\");
                Directory.CreateDirectory(pastaProduto);

                string arquivoProduto = Path.Combine(pastaProduto + "Produto.dat");

                //List<Produto> produtos = new List<Produto>();



                try
                {

                    string linha = null;

                    using (StreamReader sr = new StreamReader(arquivoProduto))
                    {

                       linha = sr.ReadLine();

                        do
                        {



                        }
                        while (linha != null);

                    }

                }
                catch
                {


                }
            }

        }


        #endregion
        public override string ToString()
        {
            return "ID: " + Id.ToString().PadLeft(5, '0')
                + "\nData de produção: " + DataProducao
                + "\nProduto: " + produto
                + "\nQuantidade: " + Quantidade.ToString("000.#0");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CadastrosBasicos;
using CadastrosBasicos.ManipulaArquivos;

namespace ComprasMateriasPrimas
{
    public class Compra
    {
        public int Id { get; set; } // 5 00000
        public DateTime DataCompra { get; set; } //8 00/00/0000
        public string Fornecedor { get; set; } //12 00.000.000/0001-00
        public float ValorTotal { get; set; } // 7 00.000,00
        public List<ItemCompra> ListaDeItens { get; set; }

        public Compra()
        {
            Id = new ManipulaArquivosCompraMP().PegarUltimoId();
            DataCompra = DateTime.Now;
        }

        public Compra(int id, DateTime dCompra, string cnpjFornecedor, float vTotal)
        {
            Id = id;
            DataCompra = dCompra;
            Fornecedor = cnpjFornecedor;
            ValorTotal = vTotal;
        }

        public void Cadastrar()
        {
            // Salvar no arquivo
            new ManipulaArquivosCompraMP().Salvar(this);
            Console.WriteLine($"Compra de codigo {Id} cadastrada!");
            Console.ReadKey();
        }

        public static Compra Localizar(int id) => new ManipulaArquivosCompraMP().Procura(id);

        public static void ImpressaoPorRegistro(List<Compra> compras)
        {
            string opt;
            if (compras.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Nada para mostrar: arquivo vazio!!");
                return;
            }
            else
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("1. Ver Compra");
                    //Console.WriteLine("2. Ver Itens da Compra");
                    Console.WriteLine("0. Voltar para o menu");
                    opt = Console.ReadLine();
                    switch (opt)
                    {
                        case "1":
                            string escolha = "<<";
                            int atual = 0;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("|         VISUALIZAÇAO DAS COMPRAS       |");
                                if (escolha.Equals("<<"))
                                {
                                    atual = 0;
                                    compras.ElementAt(atual).ImprimirCompra();
                                }
                                else if (escolha.Equals("<") && atual > 0)
                                {
                                    if (atual != 0)
                                        atual--;

                                    compras.ElementAt(atual).ImprimirCompra();
                                }
                                else if (escolha.Equals(">") && atual < compras.Count - 1)
                                {
                                    if (atual != compras.Count - 1)
                                        ++atual;

                                    compras.ElementAt(atual).ImprimirCompra();
                                }
                                else if (escolha.Equals(">>"))
                                {
                                    atual = compras.Count - 1;
                                    compras.ElementAt(atual).ImprimirCompra();
                                }
                                Console.Write(" (<<) Primeiro (<) Anterior (>) Proximo (>>) Ultimo ");
                                Console.WriteLine(@" ""x"" => Sair                           ");
                                Console.Write("Navegar: ");
                                escolha = Console.ReadLine();
                            } while (escolha != "x");
                            break;

                        //case "2":
                        //    Console.Clear();
                        //    bool sair = false;
                        //    int indice = 0;
                        //    string[] dados = File.ReadAllLines(new ManipulaArquivosCompraMP().CaminhoItemCompra);
                        //    if (dados.Length == 0)
                        //    {
                        //        Console.WriteLine("Nada pra mostrar: arquivo vazio!!");
                        //        Console.ReadKey();
                        //    }
                        //    while (!sair)
                        //    {
                        //        Console.Clear();
                        //        Console.WriteLine("1 - Inicio\n2 - Fim\n3 - Anterior\n4 - Proximo\n5 - Sair");
                        //        Console.WriteLine("Escolha a opção que deseja: ");
                        //        int opcao = int.Parse(Console.ReadLine());
                        //        switch (opcao)
                        //        {
                        //            case 1:
                        //                indice = 0;
                        //                Console.WriteLine(dados[indice]);
                        //                break;
                        //            case 2:
                        //                indice = dados.Length - 1;
                        //                Console.WriteLine(dados[indice]);
                        //                break;
                        //            case 3:
                        //                if (indice == 0)
                        //                {
                        //                    break;
                        //                }
                        //                else
                        //                {
                        //                    indice--;
                        //                    Console.WriteLine(dados[indice]);
                        //                }
                        //                break;
                        //            case 4:
                        //                if (indice == dados.Length - 1)
                        //                {
                        //                    break;
                        //                }
                        //                else
                        //                {
                        //                    indice++;
                        //                    Console.WriteLine(dados[indice]);
                        //                }
                        //                break;
                        //            case 5:
                        //                sair = true;
                        //                break;
                        //        }
                        //    }
                        //    break;

                        case "0":
                            break;

                        default:
                            Console.WriteLine("Digite 1 para Ver as compras e 2 para ver os Itens da Compra");
                            break;
                    }
                } while (opt != "0");
            }
        }

        public static void SubMenu()
        {
            new ManipulaArquivosCompraMP();

            int option = -1;
            while (option != 0)
            {
                Console.Clear();

                Console.WriteLine("=============== COMPRAS ===============");
                Console.WriteLine("1. Nova Compra");
                Console.WriteLine("2. Consultar Compra");
                Console.WriteLine("3. Imprimir Registros de Compra");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("0. Voltar");
                Console.Write("\nEscolha: ");

                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    // ---------- CADASTRAR COMPRA -----------
                    case 1:
                        if (new Read().VerificaListaFornecedor())
                            CadastraNovaCompra();
                        else
                        {
                            Console.WriteLine("Para realizar uma compra de materias primas devera ter o registro de ao menos um fornecedor.");
                            Console.ReadKey();
                        }
                            break;

                    // ---------- LOCALIZAR COMPRA -----------
                    case 2:
                        Console.WriteLine("\nLocalizar Compra\n");
                        int okl;
                        int id;
                        do
                        {
                            Console.Write("Id da Compra: ");
                            id = int.Parse(Console.ReadLine());
                            okl = (id > 0 && id < 99999) && Compra.Localizar(id) != null ? 0 : 1;
                        } while (okl != 0);
                        if (okl == 2) break;
                        Compra.Localizar(id).ImprimirCompra();
                        break;

                    // ---------- IMPRESSÃO POR REGISTRO -----------
                    case 3:
                        ImpressaoPorRegistro(new ManipulaArquivosCompraMP().PegarTodasAsCompras());
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void CadastraNovaCompra()
        {
            Compra compra = new();
            int ok;
            string cnpjFornecedor;
            do
            {
                Console.Write("CNPJ do Fornecedor: ");
                cnpjFornecedor = Console.ReadLine();

                cnpjFornecedor = cnpjFornecedor.Replace(".", "").Replace("/", "").Replace("-", "");

                ok = cnpjFornecedor != string.Empty &&
                     Validacoes.ValidarCnpj(cnpjFornecedor) &&
                     new Read().ProcurarFornecedor(cnpjFornecedor) != null ? 0 : 1;
                if (ok != 0) Console.WriteLine("CNPJ invalido ou não encontrado na base de dados, digite novamente!");
            } while (ok != 0);
            cnpjFornecedor = new Read().ProcurarFornecedor(cnpjFornecedor).CNPJ;

            int count = 1;
            List<ItemCompra> itens = new();
            Console.WriteLine("\nItens da Compra\n");
            Console.Write("Quantidade de itens a comprar (limite de 3 itens por compra): ");
            int qtdd;
            do
            {
                Console.WriteLine("Você só pode comprar 3 itens por compra!");
                qtdd = int.Parse(Console.ReadLine());
            } while (qtdd > 3 || qtdd < 0);
            do
            {
                string idMP;
                Console.WriteLine($"Item {count}");
                do
                {
                    Console.Write("- Id da Matéria Prima: ");
                    idMP = Console.ReadLine();
                } while (new MPrima().RetornaMateriaPrima(idMP) == null);
                float valorUnitario;
                float quantidade;
                do
                {
                    Console.Write("- Valor unitário do item: ");
                    valorUnitario = float.Parse(Console.ReadLine());
                    Console.Write("- Quantidade do item que deseja comprar: ");
                    quantidade = float.Parse(Console.ReadLine());
                    if ((valorUnitario * quantidade) > 99999.99f) Console.WriteLine("O valor total do Item ultrapassou o limite de 99.999,99");
                } while ((valorUnitario * quantidade) > 99999.99f);

                ItemCompra item = new(compra.Id,
                                        compra.DataCompra,
                                        idMP,
                                        quantidade,
                                        valorUnitario);
                itens.Add(item);
                count++;
            } while (count <= qtdd);
            compra.Fornecedor = cnpjFornecedor;
            itens.ForEach(item => compra.ValorTotal += item.TotalItem);
            ItemCompra.Cadastrar(itens);
            compra.Cadastrar();
        }

        public void ImprimirCompra()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Id: " + Compra.FormatarId(Id));
            Console.WriteLine("Data: " + DataCompra);
            Console.WriteLine("CNPJ Fornecedor: " + Fornecedor);
            Console.WriteLine("Valor total: " + ValorTotal.ToString("0000000").Insert(5, ",").TrimStart('0'));
            Console.WriteLine("---------------------------------");
        }

        public void ImprimirCompraEmLinha()
        {
            Console.Write("Id: " + Compra.FormatarId(Id) + " / ");
            Console.Write("Data: " + DataCompra + " / ");
            Console.Write("CNPJ Fornecedor: " + Fornecedor + " / ");
            Console.Write("Valor total: " + ValorTotal);
        }

        public override string ToString() => FormatarId(Id) + FormatarData(DataCompra) + FormatarCNPJ(Fornecedor) + FormatarValorTotal(ValorTotal);

        public static string FormatarId(int id) => id.ToString().PadLeft(5, '0');

        public static string FormatarData(DateTime data) => data.ToString("dd/MM/yyyy").Replace("/", "");

        public static string FormatarCNPJ(string cnpj) => cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

        public static string FormatarValorTotal(float total) => string.Join("", total.ToString("0.00").PadLeft(8, '0').Split(','));

        public static Compra ExtrairCompra(string linhaDoArquivo) => new Compra(
                                                                        ExtrairId(linhaDoArquivo),
                                                                        ExtrairDataCompra(linhaDoArquivo),
                                                                        ExtrairCNPJ(linhaDoArquivo),
                                                                        ExtrairValorTotal(linhaDoArquivo));

        public static int ExtrairId(string linhaDoArquivo) => int.Parse(linhaDoArquivo.Substring(0, 5).TrimStart('0'));

        public static DateTime ExtrairDataCompra(string linhaDoArquivo)
        {
            string data = linhaDoArquivo.Substring(5, 8);
            int dia = int.Parse(data.Substring(0, 2));
            int mes = int.Parse(data.Substring(2, 2));
            int ano = int.Parse(data.Substring(4, 4));

            return new DateTime(ano, mes, dia);
        }

        public static string ExtrairCNPJ(string linhaDoArquivo) => linhaDoArquivo.Substring(13, 14)
                                                                    .Insert(2, ".")
                                                                    .Insert(6, ".")
                                                                    .Insert(10, "/")
                                                                    .Insert(15, "-");

        public static float ExtrairValorTotal(string linhaDoArquivo) => float.Parse(linhaDoArquivo.Substring(26, 7));
    }
}
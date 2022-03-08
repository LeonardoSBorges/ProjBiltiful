using System;
using System.Collections.Generic;
using System.Linq;
using CadastrosBasicos;

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
            Id++;
            DataCompra = DateTime.Now;
            float soma = 0;
            ValorTotal = soma;
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
            string escolha = "primeiro";
            do
            {
                
                if (escolha.Equals("primeiro"))
                {
                    Console.Clear();
                    Console.WriteLine("|         VISUALIZAÇAO DAS COMPRAS       |");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine(" (<<) Primeiro (<) Anterior (>) Proximo (>>) Ultimo ");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("               << < > >>                  ");
                    Console.WriteLine(@" ""x"" => Sair                           ");
                }
                else
                {
                    int atual = 2;
                    Console.Clear();
                    Console.WriteLine("|         VISUALIZAÇAO DAS COMPRAS       |");
                    Console.WriteLine("------------------------------------------");
                    if (escolha.Equals("<<") || atual == 2)
                    {
                        compras.First().ImprimirCompra();
                        atual = 0;
                    }
                    else if (escolha.Equals("<") && atual > 0)
                    {
                        if (atual == 0)
                        {
                            Console.WriteLine("Não tem anterior!");
                            Console.ReadKey();
                        }
                        else
                        {
                            --atual;
                            compras.ElementAt(atual).ImprimirCompra();
                        }
                    }
                    else if (escolha.Equals(">") && atual == compras.Count)
                    {
                        if (atual == compras.Count)
                        {
                            Console.WriteLine("Não tem proximo!");
                            Console.ReadKey();
                        }
                        else
                        {
                            ++atual;
                            compras.ElementAt(atual).ImprimirCompra();
                        }
                    }
                    else if (escolha.Equals(">>") || atual == compras.Count)
                    {
                        compras.First().ImprimirCompra();
                        atual = compras.Count;
                    }
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("               << < > >>                 ");
                    Console.WriteLine(" x => Sair                               ");
                    Console.ReadKey();
                }
                Console.Write("Navegar: ");
                escolha = Console.ReadLine();
            } while (!escolha.Equals("x"));
        }

        public static void SubMenu()
        {
            int option = -1;
            while (option != 0)
            {
                Console.Clear();
                Console.WriteLine("Compra de Materias-Primas\n");
                Console.WriteLine("=============== COMPRA MATERIA PRIMA ===============");
                Console.WriteLine("1. Nova compra");
                Console.WriteLine("2. Consultar Compra");
                Console.WriteLine("3. Imprimir Registros de Compra");
                Console.WriteLine("0. Voltar");

                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    // ---------- CADASTRAR COMPRA -----------
                    case 1:
                        Compra compra = new();
                        Console.WriteLine("Cadastro de Compra\n");
                        int ok;
                        string cnpjFornecedor;
                        Console.Write("CNPJ do Fornecedor: ");
                        do
                        {
                            cnpjFornecedor = Console.ReadLine();
                            ok = cnpjFornecedor !=  string.Empty &&
                                 Validacoes.ValidarCnpj(cnpjFornecedor) && 
                                 new Read().ProcurarFornecedor(cnpjFornecedor).CNPJ.Equals(cnpjFornecedor) ? 0 : 1;

                            Console.WriteLine("CNPJ invalido ou não encontrado na base de dados, digite novamente!");
                        } while (ok != 0);

                        int count = 0;
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
                            List<string> list = new ();
                            
                            Console.Write("Id da Matéria Prima: ");
                            string idMP = new MPrima().RetornaMateriaPrima(Console.ReadLine()).Id;
                            Console.Write("Valor unitário do item: ");
                            float valorUnitario = float.Parse(Console.ReadLine());
                            ItemCompra item = new (compra.Id, 
                                                   compra.DataCompra,
                                                   idMP,
                                                   qtdd,
                                                   valorUnitario);
                            itens.Add(item);
                            count++;
                        } while (count < qtdd);

                        ItemCompra.Cadastrar(itens);
                        compra.Cadastrar();
                        break;
                    // ---------- LOCALIZAR COMPRA -----------
                    case 2:
                        Console.WriteLine("Localizar Compra\n");
                        int okl;
                        int id;
                        Console.Write("Id da Compra: ");
                        do
                        {
                            id = int.Parse(Console.ReadLine());
                            okl = (id < 0 || id > 99999) || Compra.Localizar(id) == null ? 1 : 0;
                            Console.WriteLine("CNPJ invalido ou não encontrado na base de dados!");
                            Console.Write("Tentar novamente: ");
                            Console.Write(" [1] - Novamente [0] - Sair => ");
                            okl = int.Parse(Console.ReadLine());
                        } while (okl != 0);
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

        public void ImprimirCompra()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Id: " + Compra.FormatarId(Id));
            Console.WriteLine("Data: " + DataCompra);
            Console.WriteLine("CNPJ Fornecedor: " + Fornecedor);
            Console.WriteLine("Valor total: " + ValorTotal);
            Console.WriteLine("---------------------------------");
        }

        public void ImprimirCompraEmLinha()
        {
            Console.Write("Id: " + Compra.FormatarId(Id) + " / ");
            Console.Write("Data: " + DataCompra + " / ");
            Console.Write("CNPJ Fornecedor: " + Fornecedor + " / ");
            Console.Write("Valor total: " + ValorTotal);
        }

        // 0000508082008121201200001887777700
        public override string ToString() => FormatarId(Id) + FormatarData(DataCompra) + FormatarCNPJ(Fornecedor) + FormatarValorTotal(ValorTotal);

        public static string FormatarId(int id) => id.ToString().PadLeft(5, '0');

        public static string FormatarData(DateTime data) => data.ToString("dd/MM/yyyy").Replace("/", "");

        public static string FormatarCNPJ(string cnpj) => cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

        public static string FormatarValorTotal(float total) => string.Join("", total.ToString("0.00").PadLeft(8, '0').Split('.'));

        public static Compra ExtrairCompra(string linhaDoArquivo) => new Compra(
                                                                        ExtrairId(linhaDoArquivo),
                                                                        ExtrairDataCompra(linhaDoArquivo), 
                                                                        ExtrairCNPJ(linhaDoArquivo),
                                                                        ExtrairValorTotal(linhaDoArquivo));

        public static int ExtrairId(string linhaDoArquivo) => int.Parse(linhaDoArquivo.Substring(0, 5).TrimStart(new Char[] { '0' }));

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

        public static float ExtrairValorTotal(string linhaDoArquivo)=> float.Parse(linhaDoArquivo.Substring(linhaDoArquivo.Length - 7, 7).Insert(linhaDoArquivo.Length - 2, "."));
    }
}
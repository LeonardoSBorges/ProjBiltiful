using System;
using System.Collections.Generic;
using CadastrosBasicos;

namespace ComprasMateriasPrimas
{
    public class Compra
    {
        // Titulo do módulo - Compra de Matéria-prima
        // Compra de matéria prima através de fornecedores.
        // Compras são feitas apenas de pessoas juridicas que não estejam blooqueadas 
        // Buscar do arquivo de CNPJ 
        // Arquivo bloqueados: Bloqueado.dat
        // Bloqueado.dat -> Arquivo no qual contém apenas e exclusivamente o CNPJ que são considerados problematicos de acordo com a politica
        //                  de compra da empresa
        //               -> O cnpj seguem todas as definicoes do GF, quantidade de digitos e validacao
        // ID: Numero sequencial que inicia em 1 e tera no maximo 5 digitos
        // DCOMpra: Data da compra

        public int Id { get; set; } // 5 00000
        public DateTime DCompra { get; set; } //8 00/00/0000
        public string Fornecedor { get; set; } //12 00.000.000/0001-00
        public float VTotal { get; set; } // 7 00.000,00
        public List<ItemCompra> ListaDeItens { get; set; }

        public Compra(string cnpjFornecedor)
        {
            Id++;
            DCompra = DateTime.Now;
            Fornecedor = cnpjFornecedor;
            float soma = 0;
            VTotal = soma;
        }

        public Compra(int id, DateTime dCompra, string cnpjFornecedor, float vTotal)
        {
            Id++;
            DCompra = DateTime.Now;
            Fornecedor = cnpjFornecedor;
            VTotal = vTotal;
        }

        public void Cadastrar()
        {
            // Salvar no arquivo
            new ManipulaArquivosCompraMP().Salvar(this);
            Console.WriteLine($"Compra de codigo {Id} cadastrada!");
            Console.ReadKey();
        }

        public void Localizar(int id)
        {

        }

        public void Excluir()
        {

        }

        public void ImpressaoPorRegistro()
        {

        }

        public static void SubMenu()
        {
            // Compra de materias primas
            int option = -1;
            while (option != 0)
            {
                Console.WriteLine("Compra de Materias-Primas\n");
                Console.Write(@"1. Cadastrar
                              2. Localizar
                              3. Excluir
                              4. Impressao por Registro
                              0 - Sair
                              Insira uma opcao valida: 
                              ");

                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Cadastro de Compra\n");
                        int ok;
                        string cnpjFornecedor;
                        Console.Write("CNPJ do Fornecedor: ");
                        do
                        {
                            cnpjFornecedor = Console.ReadLine();
                            ok = cnpjFornecedor == string.Empty || Validacoes.ValidarCnpj(cnpjFornecedor) ? 1 : 0;
                            Console.WriteLine("CNPJ invalido ou não encontrado na base de dados, digite novamente!");
                        } while (ok != 0);

                        //int count = 0;
                        //List<ItemCompra> itens = new();
                        //do
                        //{
                        //    Console.Write("Id da Matéria Prima: ");
                        //    string id = Console.ReadLine();
                        //    itens.Add();
                        //    count++;
                        //} while (count < 4);

                        Compra compra = new(cnpjFornecedor);
                        compra.Cadastrar();
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        // 0000508082008121201200001887777700

        public override string ToString() => FormatarId(Id) + FormatarData(DCompra) + FormatarCNPJ(Fornecedor) + FormatarVTotal(VTotal);

        public static string FormatarId(int id) => id.ToString().PadLeft(5, '0');

        public static string FormatarData(DateTime data) => data.ToString().Replace("/", "");

        public static string FormatarCNPJ(string cnpj) => cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

        public static string FormatarVTotal(float total) => string.Join("", total.ToString("0.00").PadLeft(8, '0').Split('.'));

        public static int ExtrairId(string linhaDoArquivo) => int.Parse(linhaDoArquivo.Substring(0, 5).TrimStart(new Char[] { '0' }));

        public static DateTime ExtrairDCompra(string linhaDoArquivo)
        {
            string data = linhaDoArquivo.Substring(5, 8);
            string data = linhaDoArquivo.Substring(5, 8);
	  	    int dia = int.Parse(data[0].ToString() + data[1].ToString());
	        int mes = int.Parse(data[2].ToString() + data[3].ToString());
		    int ano = int.Parse(data[4].ToString() + data[5].ToString()  + data[6].ToString()
			 + data[7].ToString());

            return new DateTime(ano, mes, dia);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;

namespace VendasProdutos
{

    public class Venda
    {
       
        private static string caminho = @"ProjBiltiful\\Venda\\Venda.dat";

        public int Id { get; set; }
        public string Cliente { get; set; }
        public string DVenda { get; set; }
        public decimal VTotal { get; set; }

        public Venda()
        {
            VTotal = 0;
        }
        public Venda(int id, string cliente, DateTime dVenda, decimal vTotal)
        {
            Id = id;
            Cliente = cliente;
            DVenda = dVenda;
            VTotal = vTotal;
        }

        public override string ToString()
        {
            return $"{Id}\t{DVenda.ToString("dd/MM/yyyy")}\n{Cliente}\n{VTotal}";
        }

        public void NovaVenda()
        {

          
            Console.WriteLine("informe o CPF do cliente:");
            string Cliente = Console.ReadLine();
            Console.Clear();
            if (VerificaCpf(Cliente) == true)
            {
                
                if (Cliente == Cliente)// aqui vai verificar se ele está cadastrado
                {
                    if (Cliente != Cliente)//se ele tiver cadastrado, aqui vai verificar se ele não está inadimplente
                        Console.WriteLine("Infelizmente não podemos realizar a venda para este CPF. Procure a administração.");
                    else
                    {
                        Id += 1;
                        Console.WriteLine("Venda número" + Id.ToString().PadLeft(5, '0'));
                        DVenda = DateTime.Now.ToString("dd/MM/yyyy");
                        Console.WriteLine(DVenda + "\n");

                        List<ItemVenda> item = new List<ItemVenda>();

                        string escolha;
                        int cont = 1;
                        do
                        {
                            Console.WriteLine("Digite o Código do Produto:");
                            string produto = Console.ReadLine();
                            Console.WriteLine("Informe a quantidade:");
                            int qtd = int.Parse(Console.ReadLine());
                            Console.WriteLine("Informe o valor do produto: ");
                            decimal valor = decimal.Parse(Console.ReadLine());
                            Console.Clear();

                            item.Add(new ItemVenda(cont, produto, qtd, valor));
                            VTotal = (valor * qtd) + VTotal;
                            item.ForEach(i => Console.WriteLine(i.ToString()));
                            if (cont < 3)
                            {
                                Console.WriteLine("Deseja incluir mais um produto?\n1 - SIM\n2 - NÃO");
                                escolha = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine();

                                if (escolha == "2")
                                {
                                    cont = 4;
                                    break;
                                }
                                else
                                    cont++;
                            }
                            else
                            {
                                Console.WriteLine("Seu carrinho está cheio!");
                                cont++;
                            }
                        } while (cont < 4);
                        Console.WriteLine("Deseja finalizar seu pedido?\n1 - SIM\n2 - NÃO");
                        escolha = Console.ReadLine();
                        if (escolha == "2")
                        {
                            Console.WriteLine("Pedido Cancelado!");
                        }
                        else
                        {
                            try
                            {
                                StreamWriter sw = new StreamWriter(caminho, append: true);

                                //item.ForEach((i) => sw.WriteLine(i.ToString()));
                                sw.WriteLine(Id.ToString().PadLeft(5, '0') + this.Cliente + DVenda + VTotal);
                                sw.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception: " + e.Message);

                            }
                            finally
                            {
                                Console.WriteLine("Pedido Finalizado!\nPressione ENTER para voltar ao MENU...");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                    }
                }
                else
                    Console.WriteLine("Cliente não cadastrado.\n");
            }
            else
            {
                Console.WriteLine("CPF inválido. Tente novamente.\n");
            }
        }

        public Venda Localizar(int id)
        {
            try
            {
                string linha = BuscaBinaria(id);

                if (linha != null)
                {
                    string idVenda = linha.Substring(0, 5);
                    string cliente = linha.Substring(5, 14);
                    string data = linha.Substring(19, 10);
                    string vtotal = linha.Substring(29, 5);

                    Venda venda = new Venda(int.Parse(idVenda), cliente, DateTime.Parse(data), Decimal.Parse(vtotal));

                    return venda;
                }

                return null;

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return null;
        }

        public string BuscaBinaria(int id)
        {
            try
            {
                string[] dados = File.ReadAllLines(caminho);

                int minimo = 0;
                int maximo = dados.Length - 1;
                int medio;

                while (minimo <= maximo)
                {
                    medio = (minimo + maximo) / 2;

                    if (id > medio)
                        minimo = medio + 1;
                    else if (id < medio)
                        maximo = medio - 1;
                    else
                        return dados[medio - 1];
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return null;
        }

        public void Excluir()
        {
        }

        public void ImpressaoPorRegistro()
        {
        }

        public static bool VerificaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

    }
}

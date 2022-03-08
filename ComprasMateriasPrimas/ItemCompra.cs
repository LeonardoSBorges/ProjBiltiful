using System;
using System.Collections.Generic;
using System.IO;

namespace ComprasMateriasPrimas
{
    public class ItemCompra
    {
        public ItemCompra(int id, DateTime dataCompra ,string materiaPrima, float quantidade, float valorUnitario)
        {
            Id = id;
            DataCompra = dataCompra;
            MateriaPrima = materiaPrima;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = quantidade * valorUnitario;
        }

        public int Id { get; set; } //5 campos
        public DateTime DataCompra { get; set; } //8 campos
        public string MateriaPrima { get; set; } //6 campos
        public float Quantidade { get; set; } //5 campos
        public float ValorUnitario { get; set; } //5 campos
        public float TotalItem { get; set; } //6 campos

        public static void Cadastrar(List<ItemCompra> itensCompra) => new ManipulaArquivosCompraMP().Salvar(itensCompra);

        public override string ToString() =>$"{Id.ToString().PadLeft(5, '0')}" +
                                            $"{DataCompra.ToString("dd/MM/yyyy").Replace("/", "")}" +
                                            $"{MateriaPrima}" +
                                            $"{Quantidade.ToString().Replace(".", "").PadLeft(5, '0')}" +
                                            $"{ValorUnitario.ToString().Replace(".", "").PadLeft(5, '0')}" +
                                            $"{TotalItem.ToString().Replace(".", "").PadLeft(6, '0')}";

        public static void Imprimir()
        {
            bool sair = false;
            int indice = 0;
            string[] dados = File.ReadAllLines(new ManipulaArquivosCompraMP().CaminhoItemCompra);
            while (!sair)
            {
                Console.WriteLine("1 - Inicio\n2 - Fim\n3 - Anterior\n4 - Proximo");
                Console.WriteLine("Escolha a opção que deseja: ");
                int opcao = int.Parse(Console.ReadLine());

                if (dados.Length <= 0)
                {
                    Console.WriteLine("Nenhum Arquivo encontrado!");
                }

                switch (opcao)
                {
                    case 1:
                        indice = 0;
                        Console.WriteLine(dados[indice]);
                        break;
                    case 2:
                        indice = dados.Length - 1;
                        Console.WriteLine(dados[indice]);
                        break;
                    case 3:
                        if (indice == 0)
                        {
                            Console.WriteLine("Não há opções anteriores.");
                        }
                        else
                        {
                            indice--;
                            Console.WriteLine(dados[indice]);
                        }
                        break;
                    case 4:
                        if (indice == dados.Length - 1)
                        {
                            Console.WriteLine("Não há opções posteriores.");
                        }
                        else
                        {
                            indice++;
                            Console.WriteLine(dados[indice]);
                        }
                        break;
                }
            }
        }

        public bool TotalMaximo() => TotalItem > 99999.99f;
    }
}
using System;
using System.Collections.Generic;

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
    }
}
using System;

namespace ComprasMateriasPrimas
{
    public class ItemCompra
    {
        public ItemCompra(int id, DateTime dCompra, string mPrima, float qtd, float vUnitario)
        {
            Id = id;
            DCompra = dCompra;
            MPrima = mPrima;
            Qtd = qtd;
            VUnitario = vUnitario;
            TItem = qtd * vUnitario;
        }

        public int Id { get; set; } //5 campos
        public DateTime DCompra { get; set; } //8 campos
        public string MPrima { get; set; } //6 campos
        public float Qtd { get; set; } //5 campos
        public float VUnitario { get; set; } //5 campos
        public float TItem { get; set; } //6 campos

        public bool TItemValido() => TItem <= 9999.99;
    }
}
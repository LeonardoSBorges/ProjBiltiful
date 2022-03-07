using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasMateriasPrimas
{
    public class ItemCompra
    {
        public int Id { get; set; }
        public DateTime DCompra { get; set; } //8 campos
        public string MPrima { get; set; } //6 campos
        public float Qtd { get; set; } //5 campos
        public float VUnitario { get; set; } //5 campos
        public float TItem { get; set; } //6 campos
    }
}

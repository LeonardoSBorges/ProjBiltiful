using System;
using System.IO;

namespace ProducaoCosmeticos
{
    public class ItemProducao
    {
        #region Atributos e Propriedades da Classe Produção

        public int id { get; set; }
        public DateTime dproducao { get; set; }
        public string mprima { get; set; }
        public float qtdmp { get; set; }

        #endregion

        #region Variáveis
        
        

        #endregion

        #region Construtor

        public ItemProducao(int id, DateTime dproducao, string mprima, float qtdmp)
        {
            this.id = id;
            this.dproducao = dproducao;
            this.mprima = mprima;
            this.qtdmp = qtdmp;
        }

        #endregion

        #region Métodos 



        #endregion
    }
}



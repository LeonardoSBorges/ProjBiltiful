using System;
using System.Globalization;
using VendasProdutos;
using CadastrosBasicos;
namespace ProjBiltiful
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cultureInformation = new CultureInfo("pt-BR");
            cultureInformation.NumberFormat.CurrencySymbol = "R$";
            CultureInfo.DefaultThreadCurrentCulture = cultureInformation;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInformation;

            new Arquivos();
            Produto a = new Produto();
        }
    }
}

using MELC.Core.DomainObjects.Dtos;
using System.Drawing;

namespace MELC.Core.DomainObjects.Resumo
{
    public class MaterialPorTipo
    {
        public MaterialPorTipo()
        {
            Materiais = new List<MaterialDesenhoDto>();
        }
        public ICollection<MaterialDesenhoDto> Materiais { get; set; }
        public decimal Lucro { get; set; }
        public decimal Imposto { get; set; }
        public decimal ImpostoIncidido => CalcularImpostos();
        public decimal LucroObtido => CalcularLucros();
        public decimal ValorTotal => CalcularValorTotal();
        public double PesoTotal => CalcularPesoTotal();
        public decimal CalcularValorTotal()
        {
            decimal total = 0;

            foreach (var material in Materiais)
            {
                material.Valor = material.Material.Preco * decimal.Parse(material.Peso.ToString());
                total += material.Valor;
            }

            return total;
        }

        public decimal CalcularImpostos()
        {
            return ValorTotal * Imposto / 100;
        }

        public decimal CalcularLucros()
        {
            var totalComImpostos = ValorTotal + ImpostoIncidido;

            return totalComImpostos * Lucro / 100;
        }

        public double CalcularPesoTotal()
        {
            double total = 0;

            foreach (var material in Materiais)
                total += material.Peso;

            return total;
        }
    }
}

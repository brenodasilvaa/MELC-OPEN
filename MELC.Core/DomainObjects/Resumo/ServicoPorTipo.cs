using MELC.Core.DomainObjects.Dtos;
using System.Globalization;

namespace MELC.Core.DomainObjects.Resumo
{
    public class ServicoPorTipo
    {
        public ServicoPorTipo()
        {
            Servicos = new List<ServicoDesenhoDto>();
        }
        public ICollection<ServicoDesenhoDto> Servicos { get; set; }
        public decimal Lucro { get; set; }
        public decimal Imposto { get; set; }
        public double HorasTrabalhadasTotal => GetHorasTrabalhadasTotal();
        public string TempoDeServicoFormatado => GetTempoServicoFormatado();
        public decimal ImpostoIncidido => CalcularImpostos();
        public decimal LucroObtido => CalcularLucros();
        public decimal Total => CalcularValorTotal();
        public decimal TotalComImpostosELucros => Total + ImpostoIncidido + LucroObtido;
        public decimal CalcularValorTotal()
        {
            decimal total = 0;

            foreach (var servico in Servicos)
                total += servico.TipoServico.Valor * (decimal)servico.TempoParametrizado;

            return total;
        }

        public decimal CalcularImpostos()
        {
            return Total * Imposto / 100;
        }

        public decimal CalcularLucros()
        {
            var totalComImpostos = Total + ImpostoIncidido;

            return totalComImpostos * Lucro / 100;
        }

        public double GetHorasTrabalhadasTotal()
        {
            double total = 0;

            foreach (var servico in Servicos)
                total += servico.TempoParametrizado;

            return total;
        }

        public string GetTempoServicoFormatado()
        {
            var splitHoras = Math.Round(HorasTrabalhadasTotal, 2).ToString().Split(NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);

            var horas = splitHoras[0];

            if (splitHoras.Length == 1)
                return $"{horas}h";

            var minutosDecimal = double.Parse(splitHoras[1]);

            var minutosBase60 = (minutosDecimal * 0.60).ToString("##");

            return $"{horas}h{minutosBase60}m";
        }
    }
}

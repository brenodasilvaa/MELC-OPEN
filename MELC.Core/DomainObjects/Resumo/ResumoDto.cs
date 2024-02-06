using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Resumo
{
    public class ResumoDto
    {
        public ResumoDto()
        {
            Servicos = new List<ServicoPorTipo>();
            Materiais = new List<MaterialPorTipo>();
            PecasNormalizadas = new List<PecaNormalizadaDto>();
            Fretes = new List<FreteDesenhoDto>();
        }
        public List<ServicoPorTipo> Servicos { get; set; }
        public List<MaterialPorTipo> Materiais { get; set; }
        public List<PecaNormalizadaDto> PecasNormalizadas { get; set; }
        public List<FreteDesenhoDto> Fretes { get; set; }
        public double HorasTrabalhadasTotal => GetHorasTrabalhadasTotal();
        public double PesoTotal => GetPesoTotal();
        public decimal ValorTotalServicos => CalcularValorTotalServicos();
        public decimal ValorTotalImpostos => CalcularValorTotalImpostos();
        public decimal ValorTotalLucros => CalcularValorTotalLucro();
        public decimal ValorTotalMateriais => CalcularValorTotalMateriais();
        public decimal ValorTotalFrete => CalcularValorTotalFrete();
        public decimal ValorTotalPecasNormalizadas => CalcularValorTotalPecasNormalizadas();
        public decimal ValorTotal => CalcularValorTotal();

        public decimal CalcularValorTotal()
        {
            CalcularValorTotalImpostos();
            CalcularValorTotalLucro();

            return ValorTotalMateriais + ValorTotalServicos + ValorTotalFrete + ValorTotalImpostos + ValorTotalLucros;
        }

        private decimal CalcularValorTotalImpostos()
        {
            decimal valorTotalImpostos = 0;

            foreach (var servico in Servicos)
                valorTotalImpostos += servico.ImpostoIncidido;

            foreach (var materiais in Materiais)
                valorTotalImpostos += materiais.ImpostoIncidido;

            foreach (var pecas in PecasNormalizadas)
                valorTotalImpostos += (decimal)pecas.ImpostoIncidido;

            foreach (var frete in Fretes)
                valorTotalImpostos += (decimal)frete.ImpostoIncidido;

            return valorTotalImpostos;
        }

        public decimal CalcularValorTotalLucro()
        {
            decimal valorTotalLucros = 0;

            foreach (var servico in Servicos)
                valorTotalLucros += servico.LucroObtido;

            foreach (var materiais in Materiais)
                valorTotalLucros += materiais.LucroObtido;

            foreach (var pecas in PecasNormalizadas)
                valorTotalLucros += (decimal)pecas.LucroObtido;

            foreach (var fretes in Fretes)
                valorTotalLucros += (decimal)fretes.LucroObtido;

            return valorTotalLucros;
        }

        public decimal CalcularValorTotalServicos()
        {
            decimal total = 0;

            foreach (var servico in Servicos)
                total += servico.Total;

            return total;
        }

        public decimal CalcularValorTotalMateriais()
        {
            decimal total = 0;

            foreach (var material in Materiais)
                total += material.ValorTotal;

            return total + CalcularValorTotalPecasNormalizadas();
        }

        public decimal CalcularValorTotalFrete()
        {
            decimal total = 0;

            foreach (var fretes in Fretes)
                total += (decimal)fretes.Total;

            return total;
        }

        public decimal CalcularValorTotalPecasNormalizadas()
        {
            decimal total = 0;

            foreach (var peca in PecasNormalizadas)
                total += (decimal)peca.Total;

            return total;
        }

        public double GetHorasTrabalhadasTotal()
        {
            double total = 0;

            foreach (var servico in Servicos)
                total += servico.HorasTrabalhadasTotal;

            return total;
        }

        public double GetPesoTotal()
        {
            double total = 0;

            foreach (var material in Materiais)
                total += material.PesoTotal;

            return total;
        }
    }
}

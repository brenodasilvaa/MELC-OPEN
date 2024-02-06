using MELC.Core.DomainObjects.Dtos;

namespace MELC.PDF.Facade.Models
{
    public class PdfDesenhos
    {
        public PdfDesenhos()
        {
            Desenhos = new List<DesenhoDto>();
        }
        public ClienteDto Cliente { get; set; }
        public PedidoDto Pedido { get; set; }
        public IEnumerable<DesenhoDto> Desenhos { get; set; }
        public string LogoImagePath { get; set; }
        public decimal ValorTotalServicos { get; private set; }
        public decimal ValorTotalMateriais { get; private set; }
        public decimal ValorTotalFretes { get; private set; }
        public decimal ValorTotalImpostos { get; private set; }
        public decimal ValorTotalLucros { get; private set; }
        public decimal ValorTotal { get; private set; }

        public void CalcularValoresTotais()
        {
            foreach (var desenho in Desenhos)
            {
                ValorTotalMateriais += desenho.Resumo.ValorTotalMateriais;
                ValorTotalServicos += desenho.Resumo.ValorTotalServicos;
                ValorTotalFretes += desenho.Resumo.ValorTotalFrete;
                ValorTotalImpostos += desenho.Resumo.ValorTotalImpostos;
                ValorTotalLucros += desenho.Resumo.ValorTotalLucros;
            }

            ValorTotal = ValorTotalMateriais + ValorTotalServicos + ValorTotalFretes + ValorTotalImpostos + ValorTotalLucros;
        }

    }
}

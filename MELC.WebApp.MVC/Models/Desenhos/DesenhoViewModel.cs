using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Models.Desenhos
{
    public class DesenhoViewModel
    {
        public ClienteDto Cliente { get; set; }
        public PedidoDto Pedido { get; set; }
        public DesenhoDto Desenho { get; set; }
        public IEnumerable<ServicoDesenhoDto> Servicos { get; set; }
        public IEnumerable<MaterialDesenhoDto> Materiais { get; set; }
        public IEnumerable<PecaNormalizadaDto> PecasNormalizadas { get; set; }
        public IEnumerable<FreteDesenhoDto> Fretes { get; set; }
        public IEnumerable<ArquivoDesenhoDto> Arquivos { get; set; }
    }
}

using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Models.Pedidos
{
    public class PedidoViewModel
    {
        public string Cliente { get; set; }
        public Guid ClienteId { get; set; }
        public PedidoDto Pedido { get; set; }
        public IEnumerable<DesenhoDto> Desenhos { get; set; }
    }
}

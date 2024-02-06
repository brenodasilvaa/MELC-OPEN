using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Models.Pedidos
{
    public class PedidosViewModel
    {
        public PedidosViewModel()
        {
            Pedidos = new List<PedidoDto>();
        }

        public string Cliente { get; set; }
        public Guid ClienteId { get; set; }
        public IEnumerable<PedidoDto> Pedidos { get; set; }
    }
}

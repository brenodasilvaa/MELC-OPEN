using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MELC.WebApp.MVC.Models.Pedidos
{
    public class NewPedidoViewModel
    {
        public Guid ClienteId { get; set; }
        public int? NumeroPedido { get; set; }

        [DisplayName("Pedido")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Title { get; set; }

        [DisplayName("Descrição")]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string? Descricao { get; set; }

        [DisplayName("Data de entrega")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? DataDeEntrega { get; set; } = null;
    }
}

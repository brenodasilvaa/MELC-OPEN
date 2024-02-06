using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MELC.WebApp.MVC.Models.Desenhos
{
    public class NewDesenhoViewModel
    {
        public Guid PedidoId { get; set; }

        [DisplayName("Desenho")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Title { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string? Conjunto { get; set; }

        [DisplayName("Descrição")]
        [StringLength(300, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string? Descricao { get; set; }

        [DisplayName("Numero do desenho")]
        public int? NumeroDesenho { get; set; }

        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Quantidade { get; set; }

        [DisplayName("Numero do conjunto")]
        public int? NumeroConjunto { get; set; }

        public List<IFormFile>? FormFiles { get; set; }
    }
}

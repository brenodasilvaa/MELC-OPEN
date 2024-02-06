using MELC.Core.Commons.Enums;
using System.ComponentModel.DataAnnotations;

namespace MELC.WebApp.MVC.Models.Clientes
{
    public class NewClienteViewModel
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 1)]
        public string Rua { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 1)]
        public string Cidade { get; set; }
        public int? Numero { get; set; }

    }
}

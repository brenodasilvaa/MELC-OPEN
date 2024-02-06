using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MELC.WebApp.MVC.Models.Faturamentos
{
    public class NewFaturamentoViewModel
    {
        public NewFaturamentoViewModel()
        {
            DesenhosIds = new List<Guid>();
        }

        public Guid PedidoId { get; set; }

        [DisplayName("Faturamento")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string? Title { get; set; }

        public string? FileName => GetFileName();
        public List<Guid> DesenhosIds { get; set; }

        private string GetFileName()
        {
            return $"{GetTitle()}.pdf";
        }

        public string GetTitle()
        {
            if (Title is null)
                return $"Faturamento-{DateTime.Now:dd-MM-yyyy-HH-mm-ss}";

            return $"{Title}";
        }
    }
}

using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Models.Desenhos
{
    public class NewDesenhoServicoViewModel
    {
        public Guid DesenhoId { get; set; }
        public Guid TipoServicoId { get; set; }
        public string? Horas { get; set; }
    }
}

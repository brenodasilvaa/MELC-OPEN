using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Models.Desenhos
{
    public class NewFreteViewModel
    {
        public Guid DesenhoId { get; set; }
        public string? Titulo { get; set; }
        public double Valor { get; set; }
    }
}

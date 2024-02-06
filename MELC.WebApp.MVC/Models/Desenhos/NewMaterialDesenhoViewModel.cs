using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Models.Desenhos
{
    public class NewMaterialDesenhoViewModel
    {
        public Guid DesenhoId { get; set; }
        public Guid MaterialId { get; set; }
        public int Quantidade { get; set; }
        public double? Largura { get; set; }
        public double? Altura { get; set; }
        public double Comprimento { get; set; }
        public double? Expessura { get; set; }
        public double? ExpessuraSuperior { get; set; }
        public double? Diametro { get; set; }
        public TipoSolido TipoSolido { get; set; }

    }
}

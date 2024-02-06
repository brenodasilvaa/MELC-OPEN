using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class Solido : Entity
    {
        public double? Largura { get; set; }
        public double? Altura { get; set; }
        public double Comprimento { get; set; }
        public double? Expessura { get; set; }
        public double? ExpessuraSuperior { get; set; }
        public double? Diametro { get; set; }
        public double Volume { get; set; }
        public TipoSolido TipoSolido { get; set; }
    }
}

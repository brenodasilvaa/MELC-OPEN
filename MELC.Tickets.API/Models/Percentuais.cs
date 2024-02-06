using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class Percentuais : Entity, IAggregateRoot
    {
        public double? Lucro { get; set; }
        public double? Impostos { get; set; }
    }
}

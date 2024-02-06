using MELC.Core.DomainObjects;
using MELC.Core.DomainObjects.Dtos;

namespace MELC.Main.API.Models
{
    public class Endereco : Entity, IAggregateRoot
    {
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public int? Numero { get; set; }
    }
}

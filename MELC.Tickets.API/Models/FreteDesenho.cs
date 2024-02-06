using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class FreteDesenho : Entity, IAggregateRoot
    {
        public Guid DesenhoId { get; set; }
        public Guid CriadoPorId { get; set; }
        public string Titulo { get; set; }
        public double Valor { get; set; }

        //EF Relation
        public virtual Desenho Desenho { get; set; }
        public virtual User CriadoPor { get; set; }
    }
}

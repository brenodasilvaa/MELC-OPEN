using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class PecaNormalizada : Entity, IAggregateRoot
    {
        public Guid DesenhoId { get; set; }
        public Guid CriadoPorId { get; set; }
        public string Title { get; set; }
        public double Quantidade { get; set; }
        public double? Preco { get; set; }

        //EF Relation
        public virtual Desenho Desenho { get; set; }
        public virtual User CriadoPor { get; set; }
    }
}

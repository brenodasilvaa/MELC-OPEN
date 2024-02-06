using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class MaterialDesenho : Entity, IAggregateRoot
    {
        public Guid DesenhoId { get; set; }
        public Guid CriadoPorId { get; set; }
        public Guid MaterialId { get; set; }
        public Guid SolidoId { get; set; }
        public double Quantidade { get; set; }
        public double Peso { get; set; }

        //EF Relation
        public virtual Desenho Desenho { get; set; }
        public virtual User CriadoPor { get; set; }
        public virtual Material Material { get; set; }
        public virtual Solido Solido { get; set; }
    }
}

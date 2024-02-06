using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class DesenhoServico : Entity, IAggregateRoot
    {
        public Guid DesenhoId { get; set; }
        public Guid? CriadoPorId { get; set; }
        public Guid TipoServicoId { get; set; }
        public int? Horas { get; set; }
        public int? Minutos { get; set; }

        //EF Relation
        public virtual Desenho Desenho { get; set; }
        public virtual User CriadoPor { get; set; }
        public virtual TipoServico TipoServico { get; set; }
    }
}

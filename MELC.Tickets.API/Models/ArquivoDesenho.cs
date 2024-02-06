using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class ArquivoDesenho : Entity, IAggregateRoot
    {
        public Guid DesenhoId { get; set; }
        public string NomeArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Extensao { get; set; }

        //EF Relation
        public virtual Desenho Desenho { get; set; }
    }
}

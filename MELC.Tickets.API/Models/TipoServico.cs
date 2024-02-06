using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class TipoServico : Entity, IAggregateRoot
    {
        public TipoServico()
        {
            DesenhoServicos = new List<DesenhoServico>();
        }

        public string Servico { get; set; }
        public decimal Valor { get; set; }

        //EF Relation
        public virtual ICollection<DesenhoServico> DesenhoServicos { get; set; }
    }
}

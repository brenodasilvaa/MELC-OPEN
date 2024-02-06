using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class User : Entity, IAggregateRoot
    {
        public User()
        {
            Desenhos = new HashSet<Desenho>();
            DesenhoServicos = new HashSet<DesenhoServico>();
            Pedidos = new HashSet<Pedido>();
            MaterialDesenho = new HashSet<MaterialDesenho>();
        }

        public string UserName { get; set; }

        // EF Relation
        public virtual IEnumerable<Desenho> Desenhos { get; set; }
        public virtual IEnumerable<DesenhoServico> DesenhoServicos { get; set; }
        public virtual IEnumerable<MaterialDesenho> MaterialDesenho { get; set; }
        public virtual IEnumerable<FreteDesenho> FretesDesenhos { get; set; }
        public virtual IEnumerable<PecaNormalizada> PecasNormalizadas { get; set; }
        public virtual IEnumerable<Faturamento> Faturamentos { get; set; }
        public virtual IEnumerable<Pedido> Pedidos { get; set; }
    }
}

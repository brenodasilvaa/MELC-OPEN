using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class Faturamento : Entity, IAggregateRoot
    {
        public string Title { get; set; }
        public string NomeArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Extensao { get; set; }
        public string Pecas { get; set; }
        public Guid CriadoPorId { get; set; }
        public Guid PedidoId { get; set; }

        //EF Relation
        public virtual User CriadoPor { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
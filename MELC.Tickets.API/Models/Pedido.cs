using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace MELC.Main.API.Models
{
    public class Pedido : Entity, IAggregateRoot
    {
        public Pedido()
        {
            Desenhos = new HashSet<Desenho>();
        }
        public int NumeroPedido { get; set; }
        public string Title { get; set; }
        public string Descricao { get; set; }
        public Guid CriadoPorId { get; set; }
        public DateTime UltimaAtualizacao { get; set; } = DateTime.Now;
        public DateTime DataDeEntrega { get; set; }
        public Status Status { get; set; }
        public Guid ClienteId { get; set; }

        //Ef Relation
        public virtual IEnumerable<Desenho> Desenhos { get; set; }
        public virtual IEnumerable<Faturamento> Faturamentos { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual User CriadoPor { get; set; }
    }
}

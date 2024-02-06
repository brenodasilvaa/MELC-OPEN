using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public Guid EnderecoId { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }

        //EF Relation

        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}

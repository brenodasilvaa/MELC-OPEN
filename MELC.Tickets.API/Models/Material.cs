using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class Material : Entity, IAggregateRoot
    {
        public Material()
        {
            MateriaisDesenhos = new HashSet<MaterialDesenho>();
        }

        public string Nome { get; set; }
        public double Densidade { get; set; }
        public decimal Preco { get; set; }

        //EF Relation
        public virtual IEnumerable<MaterialDesenho> MateriaisDesenhos { get; set; }
    }
}

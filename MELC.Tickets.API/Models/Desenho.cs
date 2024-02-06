using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects;

namespace MELC.Main.API.Models
{
    public class Desenho : Entity, IAggregateRoot
    {
        public Desenho()
        {
            DesenhoServicos = new HashSet<DesenhoServico>();
            Arquivos = new HashSet<ArquivoDesenho>();
            MateriaisDesenhos = new HashSet<MaterialDesenho>();
        }

        public int NumeroDesenho { get; set; }
        public string Title { get; set; }
        public string Descricao { get; set; }
        public string? Conjunto { get; set; }
        public int? NumeroConjunto { get; set; }
        public int Quantidade { get; set; }
        public int Prioridade { get; set; }
        public Guid CriadoPorId { get; set; }
        public Status Status { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public Guid PedidoId { get; set; }
        public double? Lucro { get; set; }
        public double? Impostos{ get; set; }

        //EF Relation
        public virtual User CriadoPor { get; set; }
        public virtual IEnumerable<DesenhoServico> DesenhoServicos { get; set; }
        public virtual IEnumerable<ArquivoDesenho> Arquivos { get; set; }
        public virtual IEnumerable<MaterialDesenho> MateriaisDesenhos { get; set; }
        public virtual IEnumerable<FreteDesenho> FretesDesenhos { get; set; }
        public virtual IEnumerable<PecaNormalizada> PecasNormalizadas { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
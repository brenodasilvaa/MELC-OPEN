using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Resumo;

namespace MELC.Core.DomainObjects.Dtos
{
    public class DesenhoDto
    {
        public DesenhoDto()
        {
            DesenhoServicos = new List<ServicoDesenhoDto>();
            Arquivos = new HashSet<ArquivoDesenhoDto>();
            MateriaisDesenhos = new HashSet<MaterialDesenhoDto>();
            PecasNormalizadas = new HashSet<PecaNormalizadaDto>();
            FretesDesenhos = new HashSet<FreteDesenhoDto>();
        }
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public Guid PedidoId { get; set; }
        public string? Conjunto { get; set; }
        public int? NumeroConjunto { get; set; }
        public int NumeroDesenho { get; set; }
        public int Quantidade { get; set; }
        public int Prioridade { get; set; }
        public string Title { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public double? Lucro { get; set; }
        public double? Impostos { get; set; }
        public IEnumerable<ServicoDesenhoDto> DesenhoServicos { get; set; }
        public IEnumerable<ArquivoDesenhoDto> Arquivos { get; set; }
        public IEnumerable<MaterialDesenhoDto> MateriaisDesenhos { get; set; }
        public IEnumerable<PecaNormalizadaDto> PecasNormalizadas { get; set; }
        public IEnumerable<FreteDesenhoDto> FretesDesenhos { get; set; }
        public ResumoDto Resumo { get; set; }
        public Guid CriadoPorId { get; set; }
        public Status Status { get; set; }
        public DateTime UltimaAtualizacao { get; set; } = DateTime.Now;
        public UserDto? CriadoPor { get; set; }
        public PedidoDto? Pedido { get; set; }

        public ResumoDto AtualizarResumoFaturamento()
        {
            var resumo = new ResumoDto();
            var servicosPorTipo = new List<ServicoPorTipo>();
            var materiaisPorTipo = new List<MaterialPorTipo>();

            var tipoServicoAgrupado = DesenhoServicos.GroupBy(x => new { x.TipoServicoId });

            foreach (var grupo in tipoServicoAgrupado)
            {
                var servicoPorTipo = new ServicoPorTipo
                {
                    Servicos = grupo.ToList(),
                    Lucro = Lucro is null ? 0 : (decimal)Lucro,
                    Imposto = Impostos is null ? 0 : (decimal)Impostos
                };

                servicosPorTipo.Add(servicoPorTipo);
            }

            var tipoMaterialAgrupado = MateriaisDesenhos.GroupBy(x => x.MaterialId);

            foreach (var grupo in tipoMaterialAgrupado)
            {
                var servicoPorTipo = new MaterialPorTipo
                {
                    Materiais = grupo.ToList(),
                    Lucro = Lucro is null ? 0 : (decimal)Lucro,
                    Imposto = Impostos is null ? 0 : (decimal)Impostos
                };

                materiaisPorTipo.Add(servicoPorTipo);
            }

            resumo.PecasNormalizadas = PecasNormalizadas.ToList();
            resumo.Fretes = FretesDesenhos.ToList();
            resumo.Servicos = servicosPorTipo;
            resumo.Materiais = materiaisPorTipo;

            Resumo = resumo;

            return Resumo;
        }

        public ResumoDto AtualizarResumo()
        {
            var resumo = new ResumoDto();
            var servicosPorTipo = new List<ServicoPorTipo>();
            var materiaisPorTipo = new List<MaterialPorTipo>();

            var tipoServicoAgrupado = DesenhoServicos.GroupBy(x => new { x.TipoServicoId, x.CriadoPor.UserName });

            foreach (var grupo in tipoServicoAgrupado)
            {
                var servicoPorTipo = new ServicoPorTipo
                {
                    Servicos = grupo.ToList(),
                    Lucro = Lucro is null ? 0 : (decimal)Lucro,
                    Imposto = Impostos is null ? 0 : (decimal)Impostos
                };

                servicosPorTipo.Add(servicoPorTipo);
            }

            var tipoMaterialAgrupado = MateriaisDesenhos.GroupBy(x => x.MaterialId);

            foreach (var grupo in tipoMaterialAgrupado)
            {
                var servicoPorTipo = new MaterialPorTipo
                {
                    Materiais = grupo.ToList(),
                    Lucro = Lucro is null ? 0 : (decimal)Lucro,
                    Imposto = Impostos is null ? 0 : (decimal)Impostos
                };

                materiaisPorTipo.Add(servicoPorTipo);
            }

            resumo.PecasNormalizadas = PecasNormalizadas.ToList();
            resumo.Fretes = FretesDesenhos.ToList();

            resumo.PecasNormalizadas.ForEach(x =>
            {
                x.Lucro = Lucro is null ? 0 : Lucro.Value;
                x.Imposto = Impostos is null ? 0 : Impostos.Value;
            });

            resumo.Fretes.ForEach(x =>
            {
                x.Lucro = Lucro is null ? 0 : Lucro.Value;
                x.Imposto = Impostos is null ? 0 : Impostos.Value;
            });

            resumo.Servicos = servicosPorTipo;
            resumo.Materiais = materiaisPorTipo;

            Resumo = resumo;

            return Resumo;
        }
    }
}

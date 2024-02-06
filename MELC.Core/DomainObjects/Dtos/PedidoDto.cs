using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Resumo;
using System.Collections.Generic;

namespace MELC.Core.DomainObjects.Dtos
{
    public class PedidoDto
    {
        public PedidoDto()
        {
            Desenhos = new HashSet<DesenhoDto>();
            Usuarios = new HashSet<UserDto>();
            Faturamentos = new HashSet<FaturamentoDto>();
        }

        public Guid Id { get; set; }
        public int NumeroPedido { get; set; }
        public string Title { get; set; }
        public string Descricao { get; set; }
        public Guid ClienteId { get; set; }
        public Guid CriadoPorId { get; set; }
        public IEnumerable<UserDto> Usuarios { get; set; }
        public IEnumerable<DesenhoDto> Desenhos { get; set; }
        public IEnumerable<FaturamentoDto> Faturamentos { get; set; }
        public ResumoDto Resumo { get; set; } = new ResumoDto();
        public Status Status { get; set; }
        public DateTime UltimaAtualizacao { get; set; } = DateTime.Now;
        public DateTime DataDeEntrega { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public UserDto CriadoPor { get; set; }
        public ClienteDto Cliente { get; set; }

        public ResumoDto AtualizarResumo()
        {
            foreach (var desenhoDto in Desenhos)
            {
                desenhoDto.AtualizarResumo();
                Resumo.Servicos.AddRange(desenhoDto.Resumo.Servicos);
                Resumo.Materiais.AddRange(desenhoDto.Resumo.Materiais);
                Resumo.PecasNormalizadas.AddRange(desenhoDto.Resumo.PecasNormalizadas);
                Resumo.Fretes.AddRange(desenhoDto.Resumo.Fretes);
            }

            return Resumo;
        }
    }
}

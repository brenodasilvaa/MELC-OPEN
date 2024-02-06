using MELC.Core.Commons.Enums;

namespace MELC.Core.DomainObjects.Dtos
{
    public class TipoServicoDto
    {
        public Guid Id { get; set; }
        public string Servico { get; set; }
        public decimal Valor { get; set; }
    }
}

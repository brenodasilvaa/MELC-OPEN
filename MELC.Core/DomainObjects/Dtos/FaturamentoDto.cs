namespace MELC.Core.DomainObjects.Dtos
{
    public class FaturamentoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string NomeArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Base64 { get; set; }
        public string Extensao { get; set; }
        public string Pecas { get; set; }
        public List<Guid> DesenhosIds { get; set; }
        public Guid CriadoPorId { get; set; }
        public Guid PedidoId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public UserDto CriadoPor { get; set; }
        public PedidoDto Pedido { get; set; }
    }
}

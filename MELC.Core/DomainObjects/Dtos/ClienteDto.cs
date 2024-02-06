namespace MELC.Core.DomainObjects.Dtos
{
    public class ClienteDto
    {
        public ClienteDto()
        {
            Pedidos = new List<PedidoDto>();
        }

        public Guid Id { get; set; }
        public Guid EnderecoId { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public ICollection<PedidoDto> Pedidos { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}

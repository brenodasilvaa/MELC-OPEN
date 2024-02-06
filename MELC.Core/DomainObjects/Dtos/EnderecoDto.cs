namespace MELC.Core.DomainObjects.Dtos
{
    public class EnderecoDto
    {
        public Guid Id { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public int? Numero { get; set; }
    }
}

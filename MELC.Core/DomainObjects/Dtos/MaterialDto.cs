namespace MELC.Core.DomainObjects.Dtos
{
    public class MaterialDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public double Densidade { get; set; }
        public decimal Preco { get; set; }
    }
}

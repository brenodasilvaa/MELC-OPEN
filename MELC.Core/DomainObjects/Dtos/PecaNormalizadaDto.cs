namespace MELC.Core.DomainObjects.Dtos
{
    public class PecaNormalizadaDto
    {
        public Guid Id { get; set; }
        public Guid DesenhoId { get; set; }
        public Guid CriadoPorId { get; set; }
        public string Title { get; set; }
        public double Quantidade { get; set; }
        public double Lucro { get; set; }
        public double Imposto { get; set; }
        public double ImpostoIncidido => CalcularImpostos();
        public double LucroObtido => CalcularLucros();
        public double Preco { get; set; }
        public double Total => CalcularValorTotal();
        public DateTime Created { get; set; } = DateTime.Now;

        public DesenhoDto Desenho { get; set; }
        public UserDto CriadoPor { get; set; }

        public double CalcularValorTotal()
        {
            return Preco * Quantidade;
        }

        public double CalcularImpostos()
        {
            return Total * Imposto / 100;
        }

        public double CalcularLucros()
        {
            var totalComImpostos = Total + ImpostoIncidido;

            return totalComImpostos * Lucro / 100;
        }
    }
}

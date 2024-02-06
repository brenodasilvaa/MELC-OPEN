namespace MELC.Core.DomainObjects.Dtos
{
    public class FreteDesenhoDto
    {
        public Guid Id { get; set; }
        public Guid DesenhoId { get; set; }
        public Guid CriadoPorId { get; set; }
        public double Valor { get; set; }
        public double Lucro { get; set; }
        public double Imposto { get; set; }
        public double ImpostoIncidido => CalcularImpostos();
        public double LucroObtido => CalcularLucros();
        public string Titulo { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public double Total => Valor;
        public virtual DesenhoDto Desenho { get; set; }
        public virtual UserDto CriadoPor { get; set; }

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

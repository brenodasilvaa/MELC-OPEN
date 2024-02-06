namespace MELC.WebApp.MVC.Models.Faturamentos
{
    public class FaturamentoDesenhoModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Conjunto { get; set; }
        public int NumeroDesenho { get; set; }
        public int? NumeroConjunto { get; set; }
        public bool Ready { get; set; }
    }
}

namespace MELC.WebApp.MVC.Models.Faturamentos
{
    public class FaturamentoAgrupador
    {
        public string Agrupador { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<FaturamentoDesenhoModel> Faturamentos { get; set; }
    }
}

using MELC.Core.Commons.Enums;

namespace MELC.WebApp.MVC.Models
{
    public class UpdateInfoModel
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string? Conjunto { get; set; }
        public int Quantidade { get; set; }
        public int? NumeroConjunto { get; set; }
        public int Prioridade { get; set; }
    }
}

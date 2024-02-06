using MELC.Core.Commons.Enums;

namespace MELC.WebApp.MVC.Models
{
    public class UpdateLucrosImpostosModel
    {
        public Guid Id { get; set; }
        public double Impostos { get; set; }
        public double Lucro { get; set; }
    }
}

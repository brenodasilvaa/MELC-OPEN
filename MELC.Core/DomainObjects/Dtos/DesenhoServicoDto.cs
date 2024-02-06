using MELC.Core.Commons.Enums;

namespace MELC.Core.DomainObjects.Dtos
{
    public class ServicoDesenhoDto
    {
        public Guid Id { get; set; }
        public Guid DesenhoId { get; set; }
        public Guid? CriadoPorId { get; set; }
        public Guid? TipoServicoId { get; set; }
        public int? Horas { get; set; }
        public int? Minutos { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DesenhoDto Desenho { get; set; }
        public TipoServicoDto TipoServico { get; set; }
        public UserDto CriadoPor { get; set; }
        public double TempoParametrizado => GetTempoServicoParametrizado();
        public string Tempo => GetTempoServicoFormatado();

        private string GetTempoServicoFormatado()
        {
            if (!Minutos.HasValue || Minutos.Value == 0)
            {
                if (!Horas.HasValue)
                    return string.Empty;

                return Horas.Value.ToString() + "h";
            }

            if (!Horas.HasValue || Horas.Value == 0)
            {
                if (!Minutos.HasValue)
                    return string.Empty;

                return Minutos.Value.ToString() + "m";
            }

            return Horas.Value.ToString() + "h" + Minutos.Value.ToString() + "m"; 
        }

        private double GetTempoServicoParametrizado()
        {
            if (!Minutos.HasValue || Minutos.Value == 0)
            {
                if (!Horas.HasValue)
                    return 0;

                return Horas.Value;
            }

            if (!Horas.HasValue || Horas.Value == 0)
            {
                if (!Minutos.HasValue)
                    return 0;

                return ConverteMinutoParaHora(Minutos.Value);
            }

            return Horas.Value + ConverteMinutoParaHora(Minutos.Value);
        }

        private static double ConverteMinutoParaHora(double minuto)
        {
            return minuto / 60;
        }
    }
}

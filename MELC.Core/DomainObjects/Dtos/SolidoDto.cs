using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Materiais.Solidos;

namespace MELC.Core.DomainObjects.Dtos
{
    public class SolidoDto
    {
        private ICalculoSolidos _calculoSolidos;

        public Guid Id { get; set; }
        public double? Largura { get; set; }
        public double? Altura { get; set; }
        public double Comprimento { get; set; }
        public double? Expessura { get; set; }
        public double? ExpessuraSuperior { get; set; }
        public double? Diametro { get; set; }
        public double Volume { get; private set; }
        public TipoSolido TipoSolido { get; set; }

        internal RetornoDto<double> CalcularVolume()
        {
            _calculoSolidos = SolidosFactory.GetCalculoSolidos(this);

            var calculoVolume = _calculoSolidos.CalcularVolume();

            if (calculoVolume.Success)
                Volume = calculoVolume.Data;

            return calculoVolume;
        }

        public string GetDimensoesFormatadas()
        {
            switch (TipoSolido)
            {
                case TipoSolido.PerfilQuadrilatero:
                    return $"L = {Largura:#.##} A = {Altura:#.##} E = {Expessura:#.##} C = {Comprimento:#.##}";
                case TipoSolido.PerfilRedondo:
                    return $"E = {Expessura:#.##} D = {Diametro:#.##} C = {Comprimento:#.##}";
                case TipoSolido.BarraRedonda:
                    return $"D = {Diametro:#.##} C = {Comprimento:#.##}";
                case TipoSolido.BarraSextavada:
                    return $"A = {Altura:#.##} C = {Comprimento:#.##}";
                case TipoSolido.BarraQuadrilatera:
                    return $"L = {Largura:#.##} A = {Altura:#.##} C = {Comprimento:#.##}";
                case TipoSolido.PerfilL:
                    return $"L = {Largura:#.##} A = {Altura:#.##} E = {Expessura:#.##} C = {Comprimento:#.##}";
                case TipoSolido.PerfilH:
                    return $"L = {Largura:#.##} A = {Altura:#.##} E = {Expessura:#.##} ES = {ExpessuraSuperior:#.##} C = {Comprimento:#.##}";
                case TipoSolido.PerfilU:
                    return $"L = {Largura:#.##} A = {Altura:#.##} E = {Expessura:#.##} C = {Comprimento:#.##}";
                default:
                    break;
            }

            return "-";
        }

        public string GetDimensoesLegendas()
        {
            switch (TipoSolido)
            {
                case TipoSolido.PerfilQuadrilatero:
                    return "L = Largura A = Altura E = Expessura C = Comprimento";
                case TipoSolido.PerfilRedondo:
                    return $"E = Expessura D = Diametro C = Comprimento";
                case TipoSolido.BarraRedonda:
                    return $"D = Diametro C = Comprimento";
                case TipoSolido.BarraSextavada:
                    return $"A = Altura C = Comprimento";
                case TipoSolido.BarraQuadrilatera:
                    return $"L = Largura A = Altura C = Comprimento";
                case TipoSolido.PerfilL:
                    return $"L = Largura A = Altura E = Expessura C = Comprimento";
                case TipoSolido.PerfilH:
                    return $"L = Largura A = Altura E = Expessura ES = Expessura Superior C = Comprimento";
                case TipoSolido.PerfilU:
                    return $"L = Largura A = Altura E = Expessura C = Comprimento";
                default:
                    break;
            }

            return "-";
        }
    }
}

using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal class CalculoBarraSextavada : ICalculoSolidos
    {
        private readonly SolidoDto _solidoDto;
        public CalculoBarraSextavada(SolidoDto solido)
        {
            _solidoDto = solido;
        }

        public RetornoDto<double> CalcularVolume()
        {
            var validacao = Validar();

            if (!validacao.Success)
                return validacao;

            var apotema = _solidoDto.Altura.Value / 2;

            return new RetornoDto<double> { Success = true, Data = 2 * Math.Sqrt(3) * Math.Pow(apotema, 2) * _solidoDto.Comprimento };
        }

        private RetornoDto<double> Validar()
        {
            if (!_solidoDto.Altura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de altura" };

            return new RetornoDto<double> { Success = true };
        }
    }
}

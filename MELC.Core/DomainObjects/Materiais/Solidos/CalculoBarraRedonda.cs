using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal class CalculoBarraRedonda : ICalculoSolidos
    {
        private readonly SolidoDto _solidoDto;
        public CalculoBarraRedonda(SolidoDto solido)
        {
            _solidoDto = solido;
        }

        public RetornoDto<double> CalcularVolume()
        {
            var validacao = Validar();

            if (!validacao.Success)
                return validacao;

            var raio = _solidoDto.Diametro.Value / 2;

            //Volume do cilindro: V = pi * R² * h

            return new RetornoDto<double> { Success = true, Data = Math.PI * Math.Pow(raio, 2) * _solidoDto.Comprimento };
        }

        private RetornoDto<double> Validar()
        {
            if (!_solidoDto.Diametro.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de diâmetro" };

            return new RetornoDto<double> { Success = true };
        }
    }
}

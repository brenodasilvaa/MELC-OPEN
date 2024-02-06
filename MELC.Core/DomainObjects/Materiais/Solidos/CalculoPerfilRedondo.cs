using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal class CalculoPerfilRedondo : ICalculoSolidos
    {
        private readonly SolidoDto _solidoDto;
        public CalculoPerfilRedondo(SolidoDto solido)
        {
            _solidoDto = solido;
        }

        public RetornoDto<double> CalcularVolume()
        {
            var validacao = Validar();

            if (!validacao.Success)
                return validacao;

            var raio = _solidoDto.Diametro.Value / 2;

            //Volume do cilindro oco: V = pi * (R² - r²) * h

            return new RetornoDto<double> { Success = true, Data = Math.PI * (Math.Pow(raio, 2) - 
                Math.Pow(raio - _solidoDto.Expessura.Value, 2)) * _solidoDto.Comprimento };
        }

        private RetornoDto<double> Validar()
        {
            if (!_solidoDto.Diametro.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de diâmetro" };

            if (!_solidoDto.Expessura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de expessura" };

            return new RetornoDto<double> { Success = true };
        }
    }
}

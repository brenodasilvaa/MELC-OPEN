using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal class CalculoPerfilU : ICalculoSolidos
    {
        private readonly SolidoDto _solidoDto;
        public CalculoPerfilU(SolidoDto solido)
        {
            _solidoDto = solido;
        }

        public RetornoDto<double> CalcularVolume()
        {
            var validacao = Validar();

            if (!validacao.Success)
                return validacao;

            var volumeFaceMaior = _solidoDto.Expessura.Value * _solidoDto.Largura.Value * _solidoDto.Comprimento * 2;
            var volumeFaceMenor = (_solidoDto.Altura.Value - 2 * _solidoDto.Expessura.Value) * _solidoDto.Expessura.Value * _solidoDto.Comprimento;

            return new RetornoDto<double> { Success = true, Data = volumeFaceMenor + volumeFaceMaior };
        }

        private RetornoDto<double> Validar()
        {
            if (!_solidoDto.Expessura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de expessura" };

            if (!_solidoDto.Largura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de largura" };

            if (!_solidoDto.Altura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de altura" };

            return new RetornoDto<double> { Success = true };
        }
    }
}

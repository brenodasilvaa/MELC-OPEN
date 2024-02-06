using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal class CalculoPerfilQuadrilatero : ICalculoSolidos
    {
        private readonly SolidoDto _solidoDto;
        public CalculoPerfilQuadrilatero(SolidoDto solido)
        {
            _solidoDto = solido;
        }

        public RetornoDto<double> CalcularVolume()
        {
            var validacao = Validar();

            if (!validacao.Success)
                return validacao;

            var volumePerfilInteiro = _solidoDto.Altura.Value * _solidoDto.Largura.Value * _solidoDto.Comprimento;

            var volumePerfilInterior = (_solidoDto.Altura.Value - 2 * _solidoDto.Expessura.Value) *
                (_solidoDto.Largura.Value - 2 * _solidoDto.Expessura.Value) * _solidoDto.Comprimento;

            if (volumePerfilInteiro == volumePerfilInterior)
                return new RetornoDto<double> { Success = true, Data = volumePerfilInteiro };

            return new RetornoDto<double> { Success = true, Data = volumePerfilInteiro - volumePerfilInterior };
        }

        private RetornoDto<double> Validar()
        {
            if (!_solidoDto.Expessura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de expessura" };

            if (!_solidoDto.Largura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de largura" };

            if (!_solidoDto.Altura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de expessura altura" };

            return new RetornoDto<double> { Success = true };
        }
    }
}

using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal class CalculoPerfilH : ICalculoSolidos
    {
        private readonly SolidoDto _solidoDto;
        public CalculoPerfilH(SolidoDto solido)
        {
            _solidoDto = solido;
        }

        public RetornoDto<double> CalcularVolume()
        {
            var validacao = Validar();

            if (!validacao.Success)
                return validacao;

            var volumeFacesExteriores = 2 * _solidoDto.ExpessuraSuperior.Value * _solidoDto.Largura.Value * _solidoDto.Comprimento;

            var volumeFaceInterior = 
                (_solidoDto.Altura.Value - 2 * _solidoDto.ExpessuraSuperior.Value) * 
                _solidoDto.Expessura.Value * 
                _solidoDto.Comprimento;

            return new RetornoDto<double> { Success = true, Data = volumeFaceInterior + volumeFacesExteriores };
        }

        private RetornoDto<double> Validar()
        {
            if (!_solidoDto.ExpessuraSuperior.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de expessura superior" };

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

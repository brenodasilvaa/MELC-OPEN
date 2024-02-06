using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal class CalculoBarraQuadrilatera : ICalculoSolidos
    {
        private readonly SolidoDto _solidoDto;
        public CalculoBarraQuadrilatera(SolidoDto solido)
        {
            _solidoDto = solido;
        }

        public RetornoDto<double> CalcularVolume()
        {
            var validacao = Validar();

            if (!validacao.Success)
                return validacao;

            var volume = _solidoDto.Largura.Value * _solidoDto.Altura.Value * _solidoDto.Comprimento; 

            return new RetornoDto<double> { Success = true, Data = volume };
        }

        private RetornoDto<double> Validar()
        {
            if (!_solidoDto.Largura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de largura" };

            if (!_solidoDto.Altura.HasValue)
                return new RetornoDto<double> { Message = "Não foi possível obter o valor de altura" };

            return new RetornoDto<double> { Success = true };
        }
    }
}

using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal interface ICalculoSolidos
    {
        public RetornoDto<double> CalcularVolume();
    }
}

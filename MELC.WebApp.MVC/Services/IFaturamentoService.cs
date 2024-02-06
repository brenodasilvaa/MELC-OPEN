using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models.Faturamentos;

namespace MELC.WebApp.MVC.Services
{
    public interface IFaturamentoService
    {
        Task<RetornoDto<IEnumerable<FaturamentoDto>>> GetAllAsync();
        Task<RetornoDto<FaturamentoDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateAsync(FaturamentoDto newFaturamento);
        Task<RetornoDto<bool>> UpdateAsync(FaturamentoDto newFaturamento);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

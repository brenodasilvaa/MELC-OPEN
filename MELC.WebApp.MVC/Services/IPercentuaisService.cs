using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Services
{
    public interface IPercentuaisService
    {
        Task<RetornoDto<IEnumerable<PercentuaisDto>>> GetAllAsync();
        Task<RetornoDto<PercentuaisDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateAsync(PercentuaisDto newPercentuais);
        Task<RetornoDto<bool>> UpdateAsync(PercentuaisDto newPercentuais);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

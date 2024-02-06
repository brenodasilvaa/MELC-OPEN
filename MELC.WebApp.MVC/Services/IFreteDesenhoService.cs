using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Services
{
    public interface IFreteDesenhoService
    {
        Task<RetornoDto<IEnumerable<FreteDesenhoDto>>> GetAllAsync();
        Task<RetornoDto<FreteDesenhoDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateAsync(FreteDesenhoDto newFreteDesenho);
        Task<RetornoDto<bool>> UpdateAsync(FreteDesenhoDto newFreteDesenho);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

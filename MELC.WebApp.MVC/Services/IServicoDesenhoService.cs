using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Services
{
    public interface IServicoDesenhoService
    {
        Task<RetornoDto<IEnumerable<ServicoDesenhoDto>>> GetAllAsync();
        Task<RetornoDto<ServicoDesenhoDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateAsync(ServicoDesenhoDto newServicoDesenho);
        Task<RetornoDto<bool>> UpdateAsync(ServicoDesenhoDto newServicoDesenho);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

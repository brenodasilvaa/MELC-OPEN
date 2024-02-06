using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Services
{
    public interface IPecaNormalizadaService
    {
        Task<RetornoDto<IEnumerable<PecaNormalizadaDto>>> GetAllAsync();
        Task<RetornoDto<PecaNormalizadaDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateAsync(PecaNormalizadaDto newPecaNormalizada);
        Task<RetornoDto<bool>> UpdateAsync(PecaNormalizadaDto newPecaNormalizada);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

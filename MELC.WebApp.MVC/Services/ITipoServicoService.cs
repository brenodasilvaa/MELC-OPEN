using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Services
{
    public interface ITipoServicoService
    {
        Task<RetornoDto<IEnumerable<TipoServicoDto>>> GetAllAsync();
        Task<RetornoDto<TipoServicoDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateAsync(TipoServicoDto newTipoServico);
        Task<RetornoDto<bool>> UpdateAsync(TipoServicoDto newTipoServico);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Services
{
    public interface IMaterialDesenhoService
    {
        Task<RetornoDto<IEnumerable<MaterialDesenhoDto>>> GetAllAsync();
        Task<RetornoDto<MaterialDesenhoDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateAsync(MaterialDesenhoDto newMaterialDesenho);
        Task<RetornoDto<bool>> UpdateAsync(MaterialDesenhoDto newMaterialDesenho);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Services
{
    public interface IMaterialService
    {
        Task<RetornoDto<IEnumerable<MaterialDto>>> GetAllAsync();
        Task<RetornoDto<MaterialDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateAsync(MaterialDto newMaterial);
        Task<RetornoDto<bool>> UpdateAsync(MaterialDto newMaterial);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

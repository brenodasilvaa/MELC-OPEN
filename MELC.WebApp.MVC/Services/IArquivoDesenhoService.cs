using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Services
{
    public interface IArquivoDesenhoService
    {
        Task<RetornoDto<ArquivoDesenhoDto>> GetByIdAsync(Guid id);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
    }
}

using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Faturamentos;

namespace MELC.WebApp.MVC.Services
{
    public interface IDesenhosService
    {
        Task<RetornoDto<IEnumerable<DesenhoDto>>> GetAllDesenhosAsync();
        Task<RetornoDto<DesenhoDto>> GetDesenhosByIdAsync(Guid id);
        Task<RetornoDto<IEnumerable<DesenhoDto>>> GetDesenhosByPedidoIdAsync(Guid id);
        Task<RetornoDto<IEnumerable<DesenhoDto>>> GetDesenhosByPrioridadeAsync();
        Task<RetornoDto<IEnumerable<FaturamentoAgrupador>>> GetDesenhosFaturamento(IEnumerable<DesenhoDto> desenhos, Status status);
        Task<RetornoDto<Guid>> CreateDesenho(DesenhoDto newDesenho);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
        Task<RetornoDto<bool>> SalvarArquivosNovoDesenho(IEnumerable<IFormFile> formFiles, Guid desenhoId);
        Task<RetornoDto<bool>> SalvarArquivos(IEnumerable<IFormFile> formFiles, Guid desenhoId);
        Task<RetornoDto<string>> ExcluirArquivo(Guid arquivoId, Guid desenhoId);
        Task<RetornoDto<bool>> UpdateInfo(UpdateInfoModel updateInfo);
        Task<RetornoDto<bool>> UpdateLucrosImpostos(UpdateLucrosImpostosModel updateLucrosImpostos);
        Task<RetornoDto<bool>> InserirServico(ServicoDesenhoDto desenhoServico);
        Task<RetornoDto<bool>> InserirMaterialDesenho(MaterialDesenhoDto materialDesenho);
        Task<RetornoDto<IEnumerable<MaterialDto>>> GetMateriais();
    }
}

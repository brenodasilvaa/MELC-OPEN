using MELC.Core.DomainObjects.Dtos;

namespace MELC.Main.API.Services
{
    public interface IDesenhosService
    {
        public Task<bool> NumeroDesenhoExisteAsync(int id);
        public Task<DesenhoDto> GetDesenhoById(Guid id);
        Task<RetornoDto<bool>> InserirMaterialDesenho(MaterialDesenhoDto materialDesenhoDto);
    }
}

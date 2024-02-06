using MELC.Core.Commons.Enums;
using MELC.Core.Data;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public interface IDesenhoRepository : IRepository<Desenho>
    {
        Task<IEnumerable<Desenho>> GetByDesenhoStatusAsync(Status status);
        Task<IEnumerable<Desenho>> GetByCriadoPorIdAsync(Guid userId);
        Task<IEnumerable<Desenho>> GetByPedidoIdAsync(Guid pedidoId);
        Task<IEnumerable<Desenho>> GetByPrioridadeAsync();
        Task<bool> NumeroDesenhoExiste(int numeroDesenho);
        Task InsereArquivosAsync(IEnumerable<ArquivoDesenho> arquivosDesenho);
        Task UpdateAll(IEnumerable<Desenho> desenhos);
    }
}

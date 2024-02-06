using MELC.Main.API.Data.Repository;

namespace MELC.Main.API.Services
{
    public class PedidosService : IPedidosService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidosService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> NumeroPedidoExisteAsync(int id)
        {
            if (await _pedidoRepository.NumeroPedidoExiste(id))
                return true;

            return false;
        }
    }
}

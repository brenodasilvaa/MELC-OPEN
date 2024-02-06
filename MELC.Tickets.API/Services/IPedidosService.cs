namespace MELC.Main.API.Services
{
    public interface IPedidosService
    {
        public Task<bool> NumeroPedidoExisteAsync(int id);
    }
}

using MELC.Core.Commons.Enums;
using MELC.Core.Data;
using MELC.Core.DomainObjects;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public interface IClienteRepository : IRepository<Cliente> 
    {
        bool ClienteExiste(Cliente cliente);
    }
}

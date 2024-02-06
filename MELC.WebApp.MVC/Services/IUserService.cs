using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Users;

namespace MELC.WebApp.MVC.Services
{
    public interface IUserService
    {
        Task<UsuarioRespostaLogin> Registro(UserDto usuarioRegistro);
        Task<RetornoDto<UsuarioUpdateDeleted>> Delete(Guid id);
        Task<RetornoDto<bool>> Update(UserDto usuarioPainel);
    }
}

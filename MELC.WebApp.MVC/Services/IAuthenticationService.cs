using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Users;

namespace MELC.WebApp.MVC.Services
{
    public interface IAuthenticationService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);
        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);
        Task<RetornoDto<IEnumerable<UserDto>>> GetAll();
        Task<UserDto> Get(Guid id);
        Task<RetornoDto<UsuarioUpdateDeleted>> Delete(Guid id);
        Task<RetornoDto<UsuarioUpdateDeleted>> Update(UserDto usuarioPainel);
    }
}

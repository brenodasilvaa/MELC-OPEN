using Microsoft.Extensions.Options;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models.Users;

namespace MELC.WebApp.MVC.Services
{
    public class UserService : Service, IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }

        public async Task<RetornoDto<UsuarioUpdateDeleted>> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/users/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<UsuarioUpdateDeleted>()
                {
                    Success = false,
                    ResponseResult = ObterObjeto<ResponseResult>(responseContent)
                };
            }

            return new RetornoDto<UsuarioUpdateDeleted>()
            {
                Success = true,
                Data = ObterObjeto<UsuarioUpdateDeleted>(responseContent)
            };
        }

        public async Task<UsuarioRespostaLogin> Registro(UserDto usuarioRegistro)
        {
            usuarioRegistro.UserName = usuarioRegistro.FullName.Split(' ').First();

            var registerContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync($"/api/users/register", registerContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ResponseResult = ObterObjeto<ResponseResult>(responseContent)
                };
            }

            return ObterObjeto<UsuarioRespostaLogin>(responseContent);
        }

        public async Task<RetornoDto<bool>> Update(UserDto usuarioPainel)
        {
            var usuario = ObterConteudo(usuarioPainel);

            var response = await _httpClient.PostAsync($"/api/users/update", usuario);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool>()
                {
                    Success = false,
                    ResponseResult = ObterObjeto<ResponseResult>(responseContent)
                };
            }

            return new RetornoDto<bool>()
            {
                Success = true,
                Data = ObterObjeto<bool>(responseContent)
            };
        }
    }
}

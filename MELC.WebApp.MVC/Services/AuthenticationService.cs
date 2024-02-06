using Microsoft.Extensions.Options;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models.Users;

namespace MELC.WebApp.MVC.Services
{
    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClientUser;

        public AuthenticationService(HttpClient httpClient,
                                     HttpClient httpClientUser,
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.AutenticacaoUrl);

            _httpClientUser = httpClientUser;
            _httpClientUser.BaseAddress = new Uri(options.Value.MainApiUrl);
        }

        public async Task<RetornoDto<UsuarioUpdateDeleted>> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/auth/delete/{id}");

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

        public async Task<RetornoDto<IEnumerable<UserDto>>> GetAll()
        {
            var response = await _httpClient.GetAsync($"/api/auth/getAll");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<UserDto>>()
                {
                    Success = false,
                    ResponseResult = ObterObjeto<ResponseResult>(responseContent)
                };
            }

            return new RetornoDto<IEnumerable<UserDto>>()
            {
                Success = true,
                Data = ObterObjeto<IEnumerable<UserDto>>(responseContent)
            };
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            var response =  await _httpClient.PostAsync($"/api/auth/login", loginContent);

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

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            usuarioRegistro.UserName = usuarioRegistro.FullName.Split(' ').First();

            var registerContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync($"/api/auth/register", registerContent);

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

        public async Task<RetornoDto<UsuarioUpdateDeleted>> Update(UserDto usuarioPainel)
        {
            var usuario = ObterConteudo(usuarioPainel);

            var response = await _httpClient.PostAsync($"/api/auth/update", usuario);

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

        public async Task<UserDto> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/auth/get/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            return ObterObjeto<UserDto>(responseContent);
        }
    }
}

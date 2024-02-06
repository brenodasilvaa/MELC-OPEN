using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MELC.Identidade.API.Models;
using MELC.WebAPI.Core.Identidade;
using Microsoft.EntityFrameworkCore;

namespace MELC.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly SignInManager<CustomUserIdentity> _signInManager;
        private readonly UserManager<CustomUserIdentity> _userInManager;
        private readonly AppSettings _appSettings;

        public AuthController(  SignInManager<CustomUserIdentity> signInManager, 
                                UserManager<CustomUserIdentity> userInManager, 
                                IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userInManager = userInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new CustomUserIdentity()
            {
                UserName = usuarioRegistro.UserName,
                FullName = usuarioRegistro.FullName,
                EmailConfirmed = true
            };

            var result = await _userInManager.CreateAsync(user, usuarioRegistro.Senha);

            if (usuarioRegistro.IsAdmin)
                await _userInManager.AddClaimAsync(user, new Claim("Admin", "Admin"));

            if (result.Succeeded)
                return CustomResponse(await GetToken(usuarioRegistro.UserName));

            foreach (var error in result.Errors)
                AdicionarErroProcessamento(error.Description);

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult>Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userInManager.FindByNameAsync(usuarioLogin.UserName);

            if (user is null)
            {
                AdicionarErroProcessamento($"Usuário {usuarioLogin.UserName} não existe");

                return CustomResponse();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GetToken(usuarioLogin.UserName));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou senha incorretos");

            return CustomResponse();
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<Usuario>> GetUsers()
        {
            var usuariosSistema = new List<Usuario>();

            var usuarios = await _userInManager.Users.ToListAsync();

            foreach (var usuario in usuarios)
            {
                usuariosSistema.Add(new Usuario
                {
                    Id = usuario.Id,
                    UserName = usuario.UserName,
                    FullName = usuario.FullName,
                    IsAdmin = (await _userInManager.GetClaimsAsync(usuario)).Any(x => x.Type == "Admin" && x.Value == "Admin")
                });
            }

            return usuariosSistema;
        }


        [HttpGet("get/{id}")]
        public async Task<Usuario> GetUsers(Guid id)
        {
            var usuario = await _userInManager.FindByIdAsync(id.ToString());

            return new Usuario
            {
                Id = usuario.Id,
                UserName = usuario.UserName,
                FullName = usuario.FullName,
                IsAdmin = (await _userInManager.GetClaimsAsync(usuario)).Any(x => x.Type == "Admin" && x.Value == "Admin")
            };
        }

        [HttpDelete("delete/{id}")]
        public async Task<UsuarioUpdateDelete> Delete(Guid id)
        {
            var usuario = await _userInManager.FindByIdAsync(id.ToString());
            var response = await _userInManager.DeleteAsync(usuario);

            foreach (var erro in response.Errors)
                AdicionarErroProcessamento(erro.Description);

            return new UsuarioUpdateDelete
            {
                Completed = response.Succeeded,
                Id = usuario.Id
            };
        }

        [HttpPost("update")]
        public async Task<UsuarioUpdateDelete> Update(Usuario usuarioUpdate)
        {
            var usuario = await _userInManager.FindByIdAsync(usuarioUpdate.Id.ToString());

            usuario.UserName = usuarioUpdate.UserName;
            usuario.FullName = usuarioUpdate.FullName;

            var isCurrentAdmin = (await _userInManager.GetClaimsAsync(usuario)).Any(x => x.Type == "Admin" && x.Value == "Admin");

            if (usuarioUpdate.IsAdmin && !isCurrentAdmin)
                await _userInManager.AddClaimAsync(usuario, new Claim("Admin", "Admin"));

            if (!usuarioUpdate.IsAdmin && isCurrentAdmin)
                await _userInManager.RemoveClaimAsync(usuario, new Claim("Admin", "Admin"));

            var response = await _userInManager.UpdateAsync(usuario);

            foreach (var erro in response.Errors)
                AdicionarErroProcessamento(erro.Description);

            return new UsuarioUpdateDelete
            {
                Completed = response.Succeeded,
                UserName = usuario.UserName,
                Id = usuario.Id
            };
        }

        private async Task<UsuarioRespostaLogin> GetToken(string name)
        {
            var user = await _userInManager.FindByNameAsync(name);
            var claims = await _userInManager.GetClaimsAsync(user);
            var userRoles = await _userInManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.FullName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.Now).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpiraEm = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}

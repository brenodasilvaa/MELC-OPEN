using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MELC.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using MELC.Core.DomainObjects.Dtos;

namespace MELC.WebApp.MVC.Controllers
{
    [Authorize]
    public class IdentityController : BaseController
    {
        private readonly Services.IAuthenticationService _authenticationService;

        public IdentityController(Services.IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) return View(usuarioLogin);

            var response = await _authenticationService.Login(usuarioLogin);

            if (ValidarResposta(response.ResponseResult)) return View(usuarioLogin);

            await RealizarLogin(response);

            if (string.IsNullOrEmpty(returnUrl)) 
                return RedirectToAction("Index", "Home");
            
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "home");
        }

        private async Task RealizarLogin(UsuarioRespostaLogin usuarioResposta)
        {
            var token = ObterTokenFormatado(usuarioResposta.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", usuarioResposta.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
        }

        private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}

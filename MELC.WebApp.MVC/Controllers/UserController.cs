using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MELC.WebApi.Core.Identidade;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models.Users;

namespace MELC.WebApp.MVC.Controllers
{
    [Authorize]
    [ClaimsAuthorize("Admin", "Admin")]
    public class UserController : BaseController
    {
        private readonly Services.IAuthenticationService _authenticationService;
        private readonly Services.IUserService _userService;

        public UserController(Services.IAuthenticationService authenticationService, Services.IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpGet]
        [Route("User/Register")]
        public IActionResult Register()
        {
            return View("~/Views/User/Register.cshtml", new UsuarioRegistro());
        }

        [HttpPost]
        [Route("User/Register")]
        public async Task<IActionResult> Register(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) 
                return Json(new { Success = false, Message = ModelState.Values.SelectMany(v => v.Errors.Select(y => y.ErrorMessage)) });

            var response = await _authenticationService.Registro(usuarioRegistro);

            if (ValidarResposta(response.ResponseResult)) 
                return Json(new { Success = false, Message = Erros });

            return Json(new { Success = true });
        }

        [HttpPost]
        [Route("User/RegisterAplicacao")]
        public async Task<IActionResult> RegisterAplicacao(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = ModelState.Values.SelectMany(v => v.Errors.Select(y => y.ErrorMessage)) });

            var usuario = (await _authenticationService.GetAll()).Data.FirstOrDefault(x => x.UserName == usuarioRegistro.UserName);

            if (usuario == null)
                return Json(new { Success = false, Message = "Não foi possível criar o usuário"});

            var userDto = new UserDto
            {
                Id = usuario.Id,
                FullName = usuarioRegistro.FullName,
                UserName = usuarioRegistro.UserName
            };

            var response = await _userService.Registro(userDto);

            if (ValidarResposta(response.ResponseResult))
                return Json(new { Success = false, Message = Erros });

            return Json(new { Success = true });
        }

        [HttpGet]
        [Route("User/GetAll")]
        public async Task<IActionResult> Index()
        {
            var response = await _authenticationService.GetAll();

            return View("~/Views/User/Index.cshtml", response);
        }

        [HttpGet]
        [Route("User/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _authenticationService.Get(id);

            return Json(new {success = true, data = response});
        }

        [HttpDelete]
        [Route("User/Delete")]
        public async Task<RetornoDto<string>> Delete(Guid id)
        {
            var response = await _authenticationService.Delete(id);

            if (ValidarResposta(response.ResponseResult))
                response.Success = false;

            return new RetornoDto<string>
            {
                Success = response.Success,
                Message = response.Message
            };
        }

        [HttpPost]
        [Route("User/UpdateAplicacao")]
        public async Task<IActionResult> UpdateAplicacao(UserDto user)
        {
            try
            {
                await _userService.Update(user);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Não foi possível concluir a operação" });
            }

        }

        [HttpPost]
        [Route("User/Update")]
        public async Task<IActionResult> Update(UserDto usuario)
        {
            await _authenticationService.Update(usuario);

            return Json(new { success = true });
        }
    }
}

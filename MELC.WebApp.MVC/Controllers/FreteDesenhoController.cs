using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models.Desenhos;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class FreteDesenhoController : BaseController
    {
        private readonly IFreteDesenhoService _FreteDesenhoService;

        public FreteDesenhoController(
            IFreteDesenhoService FreteDesenhoervice)
        {
            _FreteDesenhoService = FreteDesenhoervice;
        }

        [HttpPost]
        [Route("FreteDesenho/Create")]
        public async Task<IActionResult> Create(NewFreteViewModel frete)
        {
            try
            {
                var freteDto = new FreteDesenhoDto
                {
                    CriadoPorId = Guid.Parse(User.GetUserId()),
                    DesenhoId = frete.DesenhoId,
                    Titulo = frete.Titulo is null ? "" : frete.Titulo,
                    Valor = frete.Valor
                };
               
                var resultado = await _FreteDesenhoService.CreateAsync(freteDto);

                if (!resultado.Success)
                    return Json(new { success = false, message = resultado.ResponseResult.Errors.Messages });

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Não foi possível realizar esta operação" });
            }
        }

        [HttpDelete]
        [Route("FreteDesenho/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var Frete = await _FreteDesenhoService.GetByIdAsync(id);

                if (!User.IsAdmin() && User.GetUserUniqueName() != Frete.Data.CriadoPor.UserName)
                    return Json(new { success = false, message = "Você não pode excluir um Frete adicionado por outro usuário" });

                var resultado = await _FreteDesenhoService.DeleteAsync(id);

                if (ValidarResposta(resultado.ResponseResult))
                    return Json(new { success = false, message = resultado.Message });

                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Não foi possível realizar esta operação" });
            }
        }

        [HttpPost]
        [Route("FreteDesenho/Update")]
        public async Task<IActionResult> Update(FreteDesenhoDto FreteDesenho)
        {
            try
            {
                var Frete = await _FreteDesenhoService.GetByIdAsync(FreteDesenho.Id);

                if (!User.IsAdmin() && User.GetUserUniqueName() != Frete.Data.CriadoPor.UserName)
                    return Json(new { success = false, message = "Você não pode editar um Frete adicionado por outro usuário" });

                var resultado = await _FreteDesenhoService.UpdateAsync(FreteDesenho);

                if (ValidarResposta(resultado.ResponseResult))
                    return Json(new
                    {
                        success = false,
                        message = Erros
                    });

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Não foi possível realizar esta operação" });
            }
        }

        [HttpGet]
        [Route("FreteDesenho/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var FreteDesenho = await _FreteDesenhoService.GetAllAsync();

            if (!FreteDesenho.Success)
                return Json(new { success = false, message = FreteDesenho.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = FreteDesenho.Data });
        }

        [HttpGet]
        [Route("FreteDesenho/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var FreteDesenho = await _FreteDesenhoService.GetByIdAsync(id);

                if (!FreteDesenho.Success)
                    return Json(new { success = false, data = FreteDesenho.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = FreteDesenho.Data });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models.Desenhos;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class PecaNormalizadaController : BaseController
    {
        private readonly IPecaNormalizadaService _pecaNormalizadaService;

        public PecaNormalizadaController(
            IPecaNormalizadaService PecaNormalizadaervice)
        {
            _pecaNormalizadaService = PecaNormalizadaervice;
        }

        [HttpPost]
        [Route("PecaNormalizada/Create")]
        public async Task<IActionResult> Create(PecaNormalizadaDto pecaNormalizada)
        {
            try
            {
                var PecaNormalizadaDto = new PecaNormalizadaDto
                {
                    DesenhoId = pecaNormalizada.DesenhoId,
                    CriadoPorId = Guid.Parse(User.GetUserId()),
                    Title = pecaNormalizada.Title,
                    Preco = pecaNormalizada.Preco,
                    Quantidade = pecaNormalizada.Quantidade,
                };

                var resultado = await _pecaNormalizadaService.CreateAsync(PecaNormalizadaDto);

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
        [Route("PecaNormalizada/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var pecaNormalizada = await _pecaNormalizadaService.GetByIdAsync(id);

                if (!User.IsAdmin() && User.GetUserUniqueName() != pecaNormalizada.Data.CriadoPor.UserName)
                    return Json(new { success = false, message = "Você não pode excluir uma peça adicionada por outro usuário" });

                var resultado = await _pecaNormalizadaService.DeleteAsync(id);

                if (ValidarResposta(resultado.ResponseResult))
                    return Json(new { success = false, message = resultado.Message });

                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, data = "Não foi possível realizar esta operação" });
            }
        }

        [HttpPost]
        [Route("PecaNormalizada/Update")]
        public async Task<IActionResult> Update(PecaNormalizadaDto pecaNormalizada)
        {
            try
            {
                var peca = await _pecaNormalizadaService.GetByIdAsync(pecaNormalizada.Id);

                if (!User.IsAdmin() && User.GetUserUniqueName() != peca.Data.CriadoPor.UserName)
                    return Json(new { success = false, message = "Você não pode editar uma peça adicionada por outro usuário" });

                var resultado = await _pecaNormalizadaService.UpdateAsync(pecaNormalizada);

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
                return Json(new { success = false, data = "Não foi possível realizar esta operação" });
            }
        }

        [HttpGet]
        [Route("PecaNormalizada/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var PecaNormalizada = await _pecaNormalizadaService.GetAllAsync();

            if (!PecaNormalizada.Success)
                return Json(new { success = false, data = PecaNormalizada.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = PecaNormalizada.Data });
        }

        [HttpGet]
        [Route("PecaNormalizada/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var PecaNormalizada = await _pecaNormalizadaService.GetByIdAsync(id);

                if (!PecaNormalizada.Success)
                    return Json(new { success = false, data = PecaNormalizada.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = PecaNormalizada.Data });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

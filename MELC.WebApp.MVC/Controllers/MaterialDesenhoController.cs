using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models.Desenhos;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class MaterialDesenhoController : BaseController
    {
        private readonly IMaterialDesenhoService _materialDesenhoService;

        public MaterialDesenhoController(
            IMaterialDesenhoService MaterialDesenhoervice)
        {
            _materialDesenhoService = MaterialDesenhoervice;
        }

        [HttpPost]
        [Route("MaterialDesenho/Create")]
        public async Task<IActionResult> Create(NewMaterialDesenhoViewModel desenhoVm)
        {
            try
            {
                var materialDesenhoDto = new MaterialDesenhoDto
                {
                    DesenhoId = desenhoVm.DesenhoId,
                    CriadoPorId = Guid.Parse(User.GetUserId()),
                    MaterialId = desenhoVm.MaterialId,
                    Quantidade = desenhoVm.Quantidade,
                    Solido = new SolidoDto
                    {
                        Altura = desenhoVm.Altura,
                        Largura = desenhoVm.Largura,
                        Comprimento = desenhoVm.Comprimento,
                        Expessura = desenhoVm.Expessura,
                        ExpessuraSuperior = desenhoVm.ExpessuraSuperior,
                        Diametro = desenhoVm.Diametro,
                        TipoSolido = desenhoVm.TipoSolido
                    }
                };

                var resultado = await _materialDesenhoService.CreateAsync(materialDesenhoDto);

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
        [Route("MaterialDesenho/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var material = await _materialDesenhoService.GetByIdAsync(id);

                if (!User.IsAdmin() && User.GetUserUniqueName() != material.Data.CriadoPor.UserName)
                    return Json(new { success = false, message = "Você não pode excluir um material adicionado por outro usuário" });

                var resultado = await _materialDesenhoService.DeleteAsync(id);

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
        [Route("MaterialDesenho/Update")]
        public async Task<IActionResult> Update(MaterialDesenhoDto materialDesenho)
        {
            try
            {
                var material = await _materialDesenhoService.GetByIdAsync(materialDesenho.Id);

                if (!User.IsAdmin() && User.GetUserUniqueName() != material.Data.CriadoPor.UserName)
                    return Json(new { success = false, message = "Você não pode editar um material adicionado por outro usuário" });

                var resultado = await _materialDesenhoService.UpdateAsync(materialDesenho);

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
        [Route("MaterialDesenho/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var MaterialDesenho = await _materialDesenhoService.GetAllAsync();

            if (!MaterialDesenho.Success)
                return Json(new { success = false, message = MaterialDesenho.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = MaterialDesenho.Data });
        }

        [HttpGet]
        [Route("MaterialDesenho/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var MaterialDesenho = await _materialDesenhoService.GetByIdAsync(id);

                if (!MaterialDesenho.Success)
                    return Json(new { success = false, data = MaterialDesenho.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = MaterialDesenho.Data });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

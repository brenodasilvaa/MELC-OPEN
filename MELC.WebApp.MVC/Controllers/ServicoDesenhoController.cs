using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models.Desenhos;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class ServicoDesenhoController : BaseController
    {
        private readonly IServicoDesenhoService _servicoDesenhoService;

        public ServicoDesenhoController(
            IServicoDesenhoService servicoDesenhoService)
        {
            _servicoDesenhoService = servicoDesenhoService;
        }

        [HttpPost]
        [Route("ServicoDesenho/Create")]
        public async Task<IActionResult> Create([FromForm] NewDesenhoServicoViewModel newServico)
        {
            try
            {
                if (newServico.Horas is null)
                    return Json(new { success = false, message = "Favor inserir o tempo gasto para executar o serviço" });

                if (newServico.TipoServicoId == Guid.Empty)
                    return Json(new { success = false, message = "Favor selecionar um serviço" });

                var horas = int.Parse(newServico.Horas.Split(':').First());
                var minutos = int.Parse(newServico.Horas.Split(':').Last());

                var desenhoServicoDto = new ServicoDesenhoDto
                {
                    DesenhoId = newServico.DesenhoId,
                    Horas = horas,
                    Minutos = minutos,
                    TipoServicoId = newServico.TipoServicoId,
                    CriadoPorId = Guid.Parse(User.GetUserId())
                };

                var resultado = await _servicoDesenhoService.CreateAsync(desenhoServicoDto);

                if (!resultado.Success)
                    return Json(new { success = false, message = resultado.Message });

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Não foi possível realizar esta operação" });
            }
        }

        [HttpDelete]
        [Route("ServicoDesenho/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var servico = await _servicoDesenhoService.GetByIdAsync(id);

                if (!User.IsAdmin() && User.GetUserUniqueName() != servico.Data.CriadoPor.UserName)
                    return Json(new { success = false, message = "Você não pode excluir um serviço adicionado por outro usuário" });

                var resultado = await _servicoDesenhoService.DeleteAsync(id);

                if (ValidarResposta(resultado.ResponseResult)) 
                    return Json(new { success = false, message = resultado.Message });

                return Json(new { Success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Não foi possível realizar esta operação" });
            }
        }

        [HttpPost]
        [Route("ServicoDesenho/Update")]
        public async Task<IActionResult> Update(ServicoDesenhoDto servicoDesenho)
        {
            try
            {
                var servico = await _servicoDesenhoService.GetByIdAsync(servicoDesenho.Id);

                if (!User.IsAdmin() && User.GetUserUniqueName() != servico.Data.CriadoPor.UserName)
                    return Json(new { success = false, message = "Você não pode editar um serviço adicionado por outro usuário" });

                var result = await _servicoDesenhoService.UpdateAsync(servicoDesenho);

                if (ValidarResposta(result.ResponseResult))
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
        [Route("ServicoDesenho/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var ServicoDesenho = await _servicoDesenhoService.GetAllAsync();

            if (!ServicoDesenho.Success)
                return Json(new { success = false, message = ServicoDesenho.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = ServicoDesenho.Data });
        }

        [HttpGet]
        [Route("ServicoDesenho/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var ServicoDesenho = await _servicoDesenhoService.GetByIdAsync(id);

                if (!ServicoDesenho.Success)
                    return Json(new { success = false, message = ServicoDesenho.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = ServicoDesenho.Data });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Não foi possível realizar esta operação" });
            }
        }
    }
}

using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Clientes;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class ClientesController : BaseController
    {
        private readonly IClientesService _clientesService;

        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        [Route("Clientes")]
        public async Task<IActionResult> Index()
        {
            var clientes = await _clientesService.GetAllClientesAsync();

            return View("~/Views/Clientes/Index.cshtml", clientes);
        }

        [HttpGet]
        [Route("Clientes/Get/{id}")]
        public async Task<IActionResult> GetDetalhe(Guid id)
        {
            var cliente = await _clientesService.GetClienteByIdAsync(id);

            if (!cliente.Success)
                return Json(new { success = false, data = cliente.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = cliente.Data  });
        }

        [HttpPost]
        [Route("Clientes/Create")]
        public async Task<IActionResult> Create(NewClienteViewModel newCliente)
        {
            if (!ModelState.IsValid) return Json(new
            {
                success = false,
                data =
                   ModelState.Values.SelectMany(v => v.Errors.Select(y => y.ErrorMessage))
            });

            var clienteDto = new ClienteDto
            {
                Nome = newCliente.Nome,
                Cnpj = newCliente.Cnpj,
                Endereco = new EnderecoDto
                {
                    Rua = newCliente.Rua,
                    Numero = newCliente.Numero,
                    Cidade = newCliente.Cidade
                }
            };

            var result = await _clientesService.CreateClienteAsync(clienteDto);

            if (ValidarResposta(result.ResponseResult))
                return Json(new { success = false, data = Erros });

            return Json(new { success = true });
        }

        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Clientes/Update")]
        public async Task<IActionResult> Update(NewClienteViewModel updateCliente)
        {
            if (!ModelState.IsValid) return Json(new
            {
                success = false,
                data =
                   ModelState.Values.SelectMany(v => v.Errors.Select(y => y.ErrorMessage))
            });

            var clienteDto = new ClienteDto
            {
                Id = updateCliente.Id.Value,
                Nome = updateCliente.Nome,
                Cnpj = updateCliente.Cnpj,
                Endereco = new EnderecoDto
                {
                    Rua = updateCliente.Rua,
                    Numero = updateCliente.Numero,
                    Cidade = updateCliente.Cidade
                }
            };

            var result = await _clientesService.Update(clienteDto);

            if (ValidarResposta(result.ResponseResult))
                return Json(new { success = false, data = Erros });

            return Json(new { success = true });
        }

        [HttpDelete]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Clientes/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultado = await _clientesService.DeleteAsync(id);

            if (ValidarResposta(resultado.ResponseResult))
                return Json(new { success = false, data = Erros });

            return Json(new { success = true });
        }
    }
}

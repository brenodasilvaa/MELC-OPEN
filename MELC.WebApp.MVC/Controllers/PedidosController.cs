using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Pedidos;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class PedidosController : BaseController
    {
        private readonly IPedidosService _pedidosService;
        private readonly IClientesService _clientesService;
        private readonly IDesenhosService _desenhosService;

        public PedidosController(IPedidosService PedidosService, IClientesService clientesService, 
            IDesenhosService desenhosService)
        {
            _pedidosService = PedidosService;
            _clientesService = clientesService;
            _desenhosService = desenhosService;
        }

        [HttpGet]
        [Route("Pedidos/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var cliente = await _clientesService.GetClienteByIdAsync(id);

            if (ValidarResposta(cliente.ResponseResult)) return View("~/Views/Clientes");

            var pedidos = await _pedidosService.GetPedidosByClienteId(id);

            if (ValidarResposta(cliente.ResponseResult)) return View("~/Views/Clientes");

            var pedidosViewModel = new RetornoDto<PedidosViewModel>
            {
                Data = new PedidosViewModel
                {
                    Cliente = cliente.Data.Nome,
                    ClienteId = cliente.Data.Id,
                    Pedidos = pedidos.Data.OrderByDescending(x => x.NumeroPedido)
                }
            };

            return View("~/Views/Pedidos/Index.cshtml", pedidosViewModel);
        }

        [HttpGet]
        [Route("Pedidos/GetDetalhe/{id}")]
        public async Task<IActionResult> GetDetalhe(Guid id)
        {
            var pedido = await _pedidosService.GetPedidosByIdAsync(id);

            if (ValidarResposta(pedido.ResponseResult)) return View("~/Views/Pedidos/Index");

            if (pedido.Data.Status == Core.Commons.Enums.Status.Finished && !User.IsAdmin())
                return Unauthorized();

            pedido.Data.AtualizarResumo();

            var cliente = await _clientesService.GetClienteByIdAsync(pedido.Data.ClienteId);

            if (ValidarResposta(cliente.ResponseResult)) return View("~/Views/Pedidos");

            var desenhos = await _desenhosService.GetDesenhosByPedidoIdAsync(id);

            if (ValidarResposta(desenhos.ResponseResult)) return View("~/Views/Pedidos");

            var pedidoViewModel = new RetornoDto<PedidoViewModel>
            {
                Data = new PedidoViewModel
                {
                    Cliente = cliente.Data.Nome,
                    ClienteId = cliente.Data.Id,
                    Pedido = pedido.Data,
                    Desenhos = desenhos.Data
                }
            };

            return View("~/Views/Pedidos/pedido-detalhes.cshtml", pedidoViewModel);
        }

        [HttpPost]
        [Route("Pedidos/Create")]
        public async Task<IActionResult> Create(NewPedidoViewModel newPedido)
        {
            if(!ModelState.IsValid) return Json(new
            {
                success = false,
                data =
                    ModelState.Values.SelectMany(v => v.Errors.Select(y => y.ErrorMessage))
            });

            var pedidoDto = new PedidoDto
            {
                Title = newPedido.Title,
                NumeroPedido = newPedido.NumeroPedido ?? 0,
                Descricao = newPedido.Descricao ?? string.Empty,
                DataDeEntrega = newPedido.DataDeEntrega.Value,
                ClienteId = newPedido.ClienteId,
                CriadoPorId = Guid.Parse(User.GetUserId())
            };

            var result = await _pedidosService.CreatePedidoAsync(pedidoDto);

            if (ValidarResposta(result.ResponseResult))
                return Json(new { success = false, data = Erros });

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInfo(UpdateInfoModel updateInfo)
        {
            var resultado = await _pedidosService.UpdateInfo(updateInfo);

            if (!resultado.Success)
                return BadRequest(resultado.Message);

            if (ValidarResposta(resultado.ResponseResult))
                return Json(new { success = false, data = Erros });

            return Json(new { success = true });
        }

        [HttpDelete]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Pedidos/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultado = await _pedidosService.DeleteAsync(id);

            if (ValidarResposta(resultado.ResponseResult))
                return Json(new { success = false, data = Erros });

            return Json(new { success = true });
        }
    }
}

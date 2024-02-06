using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Dtos;
using MELC.Main.API.Models;
using MELC.Main.API.Data.Repository;
using MELC.Main.API.Services;
using MELC.Core.Commons.Enums;

namespace MELC.Main.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/pedidos")]
    public class PedidosController : BaseController
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidosService _pedidosService;
        public readonly IMapper _mapper;

        public PedidosController(IPedidoRepository trackRepository, IMapper mapper, IPedidosService pedidosService)
        {
            _pedidoRepository = trackRepository;
            _mapper = mapper;
            _pedidosService = pedidosService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoDto>>(await _pedidoRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<Pedido, PedidoDto>(await _pedidoRepository.GetByIdAsync(id)));
        }

        [HttpGet("get-by-cliente-id/{id}")]
        public async Task<ActionResult> GetByClienteId(Guid id)
        {
            return CustomResponse(_mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoDto>>
                (await _pedidoRepository.GetByClienteIdAsync(id)));
        }

        [HttpGet("getByCriadoPorId/{id}")]
        public async Task<ActionResult> GetByCriadoPorId(Guid id)
        {
            return CustomResponse(_mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoDto>>(await _pedidoRepository.GetByCriadoPorIdAsync(id)));
        }

        [HttpGet("getByPedidoStatus/{status}")]
        public async Task<ActionResult> GetByPedidoStatus(Status status)
        {
            return CustomResponse(_mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoDto>>(await _pedidoRepository.GetByStatusAsync(status)));
        }

        [HttpPost("new-pedido")]
        public async Task<ActionResult> CriarNovoPedido(PedidoDto newPedido)
        {
            if (await _pedidosService.NumeroPedidoExisteAsync(newPedido.NumeroPedido))
            {
                AdicionarErroProcessamento($"O número de pedido {newPedido.NumeroPedido} já existe");
                return CustomResponse();
            }

            var ultimoPedido = (await _pedidoRepository.GetByClienteIdAsync(newPedido.ClienteId)).OrderByDescending(x => x.NumeroPedido).FirstOrDefault();

            if (ultimoPedido is null)
                newPedido.NumeroPedido = 1;

            else if (newPedido.NumeroPedido == 0)
                newPedido.NumeroPedido = ultimoPedido.NumeroPedido + 1;

            return CustomResponse(await _pedidoRepository.InsertAsync(_mapper.Map<PedidoDto, Pedido>(newPedido)));
        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            await _pedidoRepository.DeleteByIdAsync(id);

            return CustomResponse(true);
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(PedidoDto pedidoDto)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoDto.Id);

            pedido.Status = pedidoDto.Status;
            pedido.Descricao = pedidoDto.Descricao;
            pedido.UltimaAtualizacao = pedidoDto.UltimaAtualizacao;

            await _pedidoRepository.Update(pedido);

            return Ok(true);
        }
    }
}

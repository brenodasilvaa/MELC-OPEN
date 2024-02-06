using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MELC.Main.API.Models;
using MELC.Main.API.Data.Repository;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;

namespace MELC.Main.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/clientes")]
    public class ClientesController : BaseController
    {
        private readonly IClienteRepository _clienteRepository;
        public readonly IMapper _mapper;

        public ClientesController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDto>>(await _clienteRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<Cliente, ClienteDto>(await _clienteRepository.GetByIdAsync(id)));
        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            await _clienteRepository.DeleteByIdAsync(id);

            return CustomResponse(true);
        }

        [HttpPost("new-cliente")]
        public async Task<ActionResult> CriarNovoCliente(ClienteDto newCliente)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (_clienteRepository.ClienteExiste(_mapper.Map<ClienteDto, Cliente>(newCliente)))
            {
                AdicionarErroProcessamento($"Cliente já existe");

                return CustomResponse();
            }

            return CustomResponse( await _clienteRepository.InsertAsync(_mapper.Map<ClienteDto, Cliente>(newCliente)));
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(ClienteDto clienteDto)
        {
            await _clienteRepository.Update(_mapper.Map<ClienteDto, Cliente>(clienteDto));

            return Ok(true);
        }
    }
}

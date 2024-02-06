using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MELC.Main.API.Models;
using AutoMapper;
using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Dtos;
using MELC.Main.API.Data.Repository;
using MELC.Main.API.Services;

namespace MELC.Main.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/desenhos")]
    public class DesenhosController : BaseController
    {
        private readonly IDesenhoRepository _desenhoRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IDesenhosService _desenhoService;
        public readonly IMapper _mapper;

        public DesenhosController(
            IDesenhoRepository trackRepository, 
            IMapper mapper, 
            IDesenhosService desenhosService, 
            IMaterialRepository materialRepository, 
            IServicoRepository servicoRepository)
        {
            _desenhoRepository = trackRepository;
            _mapper = mapper;
            _desenhoService = desenhosService;
            _materialRepository = materialRepository;
            _servicoRepository = servicoRepository;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<Desenho>, IEnumerable<DesenhoDto>>(await _desenhoRepository.GetAllAsync()));
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(await _desenhoService.GetDesenhoById(id));
        }

        [HttpGet("getByCriadoPorId/{id}")]
        public async Task<ActionResult> GetByCriadoPorId(Guid id)
        {
            return CustomResponse(_mapper.Map<IEnumerable<Desenho>, IEnumerable<DesenhoDto>>(await _desenhoRepository.GetByCriadoPorIdAsync(id)));
        }

        [HttpGet("getByDesenhoStatus/{status}")]
        public async Task<ActionResult> GetByStatus(Status status)
        {
            return CustomResponse(_mapper.Map<IEnumerable<Desenho>, IEnumerable<DesenhoDto>>(await _desenhoRepository.GetByDesenhoStatusAsync(status)));
        }


        [HttpGet("getAllByPedidoId/{id}")]
        public async Task<ActionResult> GetAllByPedidoId(Guid id)
        {
            return CustomResponse(_mapper.Map<IEnumerable<Desenho>, IEnumerable<DesenhoDto>>(await _desenhoRepository.GetByPedidoIdAsync(id)));
        }

        [HttpGet("get-all-by-prioridade")]
        public async Task<ActionResult> GetAllByPrioridade(Guid id)
        {
            return CustomResponse(_mapper.Map<IEnumerable<Desenho>, IEnumerable<DesenhoDto>>(await _desenhoRepository.GetByPrioridadeAsync()));
        }

        [HttpPost("new-desenho")]
        public async Task<ActionResult> CreateNewDesenho(DesenhoDto desenhoDto)
        {
            if (await _desenhoService.NumeroDesenhoExisteAsync(desenhoDto.NumeroDesenho))
            {
                AdicionarErroProcessamento($"O número de desenho {desenhoDto.NumeroDesenho} já existe");
                return CustomResponse();
            }

            var ultimoPedido = (await _desenhoRepository.GetByPedidoIdAsync(desenhoDto.PedidoId)).OrderByDescending(x => x.NumeroDesenho).FirstOrDefault();

            if (desenhoDto.NumeroDesenho == 0)
            {
                if (ultimoPedido is null)
                    desenhoDto.NumeroDesenho = 1;
                else
                    desenhoDto.NumeroDesenho = ultimoPedido.NumeroDesenho + 1;
            }

            return CustomResponse(await _desenhoRepository.InsertAsync(_mapper.Map<DesenhoDto, Desenho>(desenhoDto)));
        }

        [HttpDelete("delete-by-id/{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            await _desenhoRepository.DeleteByIdAsync(id);

            return Ok(true);
        }

        [HttpPost("novos-arquivos")]
        public async Task<ActionResult> CreateNewDesenho(IEnumerable<ArquivoDesenhoDto> arquivoDesenhoDto)
        {
            await _desenhoRepository.InsereArquivosAsync(_mapper.Map<IEnumerable<ArquivoDesenhoDto>, IEnumerable<ArquivoDesenho>>(arquivoDesenhoDto));

            return Ok(true);
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(DesenhoDto desenhoDto)
        {
            var desenho = await _desenhoRepository.GetByIdAsync(desenhoDto.Id);

            desenho.Status = desenhoDto.Status;
            desenho.Descricao = desenhoDto.Descricao;
            desenho.Conjunto = desenhoDto.Conjunto;
            desenho.NumeroConjunto = desenhoDto.NumeroConjunto;
            desenho.Quantidade = desenhoDto.Quantidade;
            desenho.Prioridade = desenhoDto.Prioridade;
            desenho.UltimaAtualizacao = desenhoDto.UltimaAtualizacao;

            desenho.Pedido.UltimaAtualizacao = desenhoDto.UltimaAtualizacao;

            await _desenhoRepository.Update(desenho);

            return Ok(true);
        }

        [HttpPost("update-lucros-impostos")]
        public async Task<ActionResult> UpdateLucrosImpostos(DesenhoDto desenhoDto)
        {
            var desenho = await _desenhoRepository.GetByIdAsync(desenhoDto.Id);

            desenho.Impostos = desenhoDto.Impostos;
            desenho.Lucro = desenhoDto.Lucro;

            desenho.Pedido.UltimaAtualizacao = desenhoDto.UltimaAtualizacao;

            await _desenhoRepository.Update(desenho);

            return Ok(true);
        }

        [HttpPost("inserir-servico")]
        public async Task<ActionResult> InserirServico(ServicoDesenhoDto desenhoServicoDto)
        {
            var desenho = await _desenhoRepository.GetByIdAsync(desenhoServicoDto.DesenhoId);

            desenho.UltimaAtualizacao = DateTime.Now;

            await _servicoRepository.InsertAsync(_mapper.Map<ServicoDesenhoDto, DesenhoServico>(desenhoServicoDto));

            return Ok(true);
        }

        [HttpPost("inserir-material-desenho")]
        public async Task<ActionResult> InserirMaterialDesenho(MaterialDesenhoDto materialDesenhoDto)
        {
            var resultado = await _desenhoService.InserirMaterialDesenho(materialDesenhoDto);

            if (!resultado.Success)
                AdicionarErroProcessamento(resultado.Message);

            return CustomResponse(true);
        }

        [HttpGet("get-materiais")]
        public async Task<ActionResult> GetMateriais()
        {
            return CustomResponse(_mapper.Map<IEnumerable<Material>, IEnumerable<MaterialDto>>(await _materialRepository.GetAllAsync()));
        }
    }
}

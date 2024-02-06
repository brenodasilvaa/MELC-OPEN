using AutoMapper;
using MELC.Core.Commons.FileHelper;
using MELC.Core.DomainObjects.Dtos;
using MELC.Main.API.Data.Repository;
using MELC.Main.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MELC.Main.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/faturamento")]
    public class FaturamentoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IFaturamentoRepository _faturamentoRepository;
        private readonly IDesenhoRepository _desenhoRepository;
        public FaturamentoController(
            IMapper mapper,
            IFaturamentoRepository faturamentoRepository,
            IDesenhoRepository desenhoRepository)
        {
            _mapper = mapper;
            _faturamentoRepository = faturamentoRepository;
            _desenhoRepository = desenhoRepository;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<Faturamento>, IEnumerable<FaturamentoDto>>(await _faturamentoRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var faturamento = _mapper.Map<Faturamento, FaturamentoDto>(await _faturamentoRepository.GetByIdAsync(id));

            faturamento.Base64 = FileHelper.ConvertPdfToBase64(faturamento.CaminhoArquivo);
            
            return CustomResponse(faturamento);
        }

        [HttpPost("new")]
        public async Task<ActionResult> New(FaturamentoDto newFaturamento)
        {
            var desenhos = new List<Desenho>();

            foreach (var desenhoId in newFaturamento.DesenhosIds)
            {
                var desenho = await _desenhoRepository.GetByIdAsync(desenhoId);
                desenho.Status = Core.Commons.Enums.Status.Billed;
                desenho.Prioridade = 0;
                desenhos.Add(desenho);
            }

            await _desenhoRepository.UpdateAll(desenhos);

            return CustomResponse(await _faturamentoRepository.InsertAsync(_mapper.Map<FaturamentoDto, Faturamento>(newFaturamento)));
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(FaturamentoDto Faturamento)
        {
            await _faturamentoRepository.Update(_mapper.Map<FaturamentoDto, Faturamento>(Faturamento));

            return Ok(true);
        }
    }
}

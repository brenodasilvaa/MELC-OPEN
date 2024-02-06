using MELC.Core.Commons.FileHelper;
using MELC.Core.DomainObjects.Dtos;
using MELC.PDF.Facade;
using MELC.PDF.Facade.Models;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models.Faturamentos;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class FaturamentoService : Service, IFaturamentoService
    {
        private readonly HttpClient _httpClient;
        private readonly IDesenhosService _desenhoService;
        private readonly IClientesService _clienteService;
        private readonly IPedidosService _pedidoService;
        private readonly IWebHostEnvironment _environment;
        private readonly IPdf _pdfGenerator;
        private readonly string _pathArquivos = FileHelper.GetPathArquivos("Faturamentos");

        public FaturamentoService(HttpClient httpClient,
                                    IOptions<AppSettings> options,
                                    IWebHostEnvironment environment,
                                    IDesenhosService desenhos,
                                    IClientesService clientesService,
                                    IPedidosService pedidoService,
                                    IPdf pdfGenerator)
        {
            _desenhoService = desenhos;
            _clienteService = clientesService;
            _pedidoService = pedidoService;
            _pdfGenerator = pdfGenerator;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
            _environment = environment;
        }

        public async Task<RetornoDto<Guid>> CreateAsync(FaturamentoDto faturamentoDto)
        {
            var resultado = await GenerateFaturamentoPdf(faturamentoDto);

            if (!resultado.Success)
                return new RetornoDto<Guid>() {Success = false, Message = resultado.Message};

            var FaturamentoContent = ObterConteudo(resultado.Data);

            var response = await _httpClient.PostAsync($"/api/faturamento/new", FaturamentoContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Success = true, Data = ObterObjeto<Guid>(responseContent) };
        }

        private async Task<RetornoDto<FaturamentoDto>> GenerateFaturamentoPdf(FaturamentoDto faturamentoDto)
        {
            var desenhos = new List<DesenhoDto>();

            foreach (var desenhoId in faturamentoDto.DesenhosIds)
            {
                var desenho = (await _desenhoService.GetDesenhosByIdAsync(desenhoId)).Data;
                desenho.AtualizarResumo();
                desenhos.Add(desenho);
            }

            var desenhoPdfModel = new PdfDesenhos
            {
                Desenhos = desenhos,
                Cliente = (await _clienteService.GetClienteByIdAsync(desenhos.First().Pedido.ClienteId)).Data,
                Pedido = (await _pedidoService.GetPedidosByIdAsync(desenhos.First().Pedido.Id)).Data,
                LogoImagePath = Path.Combine(_environment.WebRootPath, "assets/img/melc-logo.png")
            };

            var pathPedido = Path.Combine(_pathArquivos, desenhoPdfModel.Pedido.Id.ToString());
            var pathFaturamentoPdf = Path.Combine(pathPedido, faturamentoDto.NomeArquivo);

            using (var pdfStream = await _pdfGenerator.GeneratePdf(desenhoPdfModel))
            {
                await FileHelper.CopyStreamToFileAsync(pathPedido, pdfStream, pathFaturamentoPdf);
            };

            faturamentoDto.CaminhoArquivo = pathFaturamentoPdf;
            faturamentoDto.PedidoId = desenhos.First().PedidoId;

            return new RetornoDto<FaturamentoDto> { Success = true, Data = faturamentoDto };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/faturamento/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<FaturamentoDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/faturamento/get-all");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<FaturamentoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<FaturamentoDto>> {Success = true, Data = ObterObjeto<IEnumerable<FaturamentoDto>>(responseContent) };
        }

        public async Task<RetornoDto<FaturamentoDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/faturamento/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<FaturamentoDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<FaturamentoDto> { Success = true, Data = ObterObjeto<FaturamentoDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateAsync(FaturamentoDto Faturamento)
        {
            var conteudoFaturamento = ObterConteudo(Faturamento);

            var responseUpdate = await _httpClient.PostAsync($"/api/faturamento/update", conteudoFaturamento);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}

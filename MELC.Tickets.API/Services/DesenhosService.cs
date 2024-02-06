using AutoMapper;
using MELC.Core.Commons;
using MELC.Core.Commons.Enums;
using MELC.Core.Commons.FileHelper;
using MELC.Core.DomainObjects.Dtos;
using MELC.Main.API.Data.Repository;
using MELC.Main.API.Models;

namespace MELC.Main.API.Services
{
    public class DesenhosService : IDesenhosService
    {
        private readonly IDesenhoRepository _desenhoRepository;
        private readonly IMaterialDesenhoRepository _materialDesenhoRepository;
        private readonly IMaterialRepository _materialRepository;
        public readonly IMapper _mapper;

        public DesenhosService(
            IDesenhoRepository desenhoRepository, 
            IMapper mapper, 
            IMaterialDesenhoRepository materialDesenhoRepository, 
            IMaterialRepository materialRepository)
        {
            _desenhoRepository = desenhoRepository;
            _mapper = mapper;
            _materialDesenhoRepository = materialDesenhoRepository;
            _materialRepository = materialRepository;
        }

        public async Task<bool> NumeroDesenhoExisteAsync(int id)
        {
            if (await _desenhoRepository.NumeroDesenhoExiste(id))
                return true;

            return false;
        }

        public async Task<DesenhoDto> GetDesenhoById(Guid id)
        {
            var desenhoDto =  _mapper.Map<Desenho, DesenhoDto>(await _desenhoRepository.GetByIdAsync(id));

            foreach (var arquivo in desenhoDto.Arquivos)
            {
                arquivo.Base64 = FileHelper.ConvertPdfToBase64(arquivo.CaminhoArquivo);
            }

            return desenhoDto;
        }

        public async Task<RetornoDto<bool>> InserirMaterialDesenho(MaterialDesenhoDto materialDesenhoDto)
        {
            materialDesenhoDto.Material = 
                _mapper.Map<Material, MaterialDto>(await _materialRepository.GetByIdAsync(materialDesenhoDto.MaterialId));

            var resultado = materialDesenhoDto.AtualizarPropriedadesMaterialDesenho();

            materialDesenhoDto.Material = null;

            if (!resultado.Success)
                return new RetornoDto<bool> { Success = false, Message = resultado.Message };

            var desenho = await _desenhoRepository.GetByIdAsync(materialDesenhoDto.DesenhoId);

            desenho.UltimaAtualizacao = DateTime.Now;
            desenho.Pedido.UltimaAtualizacao = DateTime.Now;

            await _materialDesenhoRepository.InsertAsync(_mapper.Map<MaterialDesenhoDto, MaterialDesenho>(materialDesenhoDto));

            return new RetornoDto<bool> { Success = true };
        }
    }
}

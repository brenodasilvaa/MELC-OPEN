using AutoMapper;
using MELC.Core.DomainObjects.Dtos;
using MELC.Main.API.Data;
using MELC.Main.API.Data.Repository;
using MELC.Main.API.Models;
using MELC.Main.API.Services;

namespace MELC.Main.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDesenhoRepository, DesenhoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMaterialDesenhoRepository, MaterialDesenhoRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IServicoRepository, ServicoRepository>();
            services.AddScoped<ITipoServicoRepository, TipoServicoRepository>();
            services.AddScoped<IServicoDesenhoRepository, ServicoDesenhoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPecaNormalizadaRepository, PecaNormalizadaRepository>();
            services.AddScoped<IFaturamentoRepository, FaturamentoRepository>();
            services.AddScoped<IPercentuaisRepository, PercentuaisRepository>();
            services.AddScoped<IArquivoDesenhoRepository, ArquivoDesenhoRepository>();
            services.AddScoped<IFreteDesenhoRepository, FreteDesenhoRepository>();

            services.AddScoped<IPedidosService, PedidosService>();
            services.AddScoped<IDesenhosService, DesenhosService>();
            services.AddScoped<MelcContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Cliente, ClienteDto>();
                CreateMap<ClienteDto, Cliente>();
                CreateMap<Pedido, PedidoDto>();
                CreateMap<PedidoDto, Pedido>();
                CreateMap<Desenho, DesenhoDto>();
                CreateMap<DesenhoDto, Desenho>();
                CreateMap<DesenhoServico, ServicoDesenhoDto>();
                CreateMap<ServicoDesenhoDto, DesenhoServico>();
                CreateMap<User, UserDto>();
                CreateMap<UserDto, User>();
                CreateMap<TipoServico, TipoServicoDto>();
                CreateMap<TipoServicoDto, TipoServico>();
                CreateMap<ArquivoDesenho, ArquivoDesenhoDto>();
                CreateMap<ArquivoDesenhoDto, ArquivoDesenho>();
                CreateMap<MaterialDesenho, MaterialDesenhoDto>();
                CreateMap<MaterialDesenhoDto, MaterialDesenho>();
                CreateMap<Solido, SolidoDto>();
                CreateMap<SolidoDto, Solido>();
                CreateMap<Material, MaterialDto>();
                CreateMap<MaterialDto, Material>();
                CreateMap<PecaNormalizada, PecaNormalizadaDto>();
                CreateMap<PecaNormalizadaDto, PecaNormalizada>();
                CreateMap<Faturamento, FaturamentoDto>();
                CreateMap<FaturamentoDto, Faturamento>();
                CreateMap<Endereco, EnderecoDto>();
                CreateMap<EnderecoDto, Endereco>();
                CreateMap<Percentuais, PercentuaisDto>();
                CreateMap<PercentuaisDto, Percentuais>();
                CreateMap<FreteDesenhoDto, FreteDesenho>();
                CreateMap<FreteDesenho, FreteDesenhoDto>();
            }
        }
    }
}


using MELC.WebApp.MVC.Services;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Services.Handlers;
using MELC.PDF.Facade;

namespace MELC.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>();

            services.AddHttpClient<IDesenhosService, DesenhosService>();
            services.AddHttpClient<IDesenhosService, DesenhosService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IDesenhosService, DesenhosService>();
            services.AddHttpClient<IDesenhosService, DesenhosService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IPedidosService, PedidosService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IClientesService, ClientesService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<ITipoServicoService, TipoServicoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IMaterialService, MaterialService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IMaterialDesenhoService, MaterialDesenhoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IServicoDesenhoService, ServicoDesenhoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IUserService, UserService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IPecaNormalizadaService, PecaNormalizadaService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>(); 
            
            services.AddHttpClient<IFaturamentoService, FaturamentoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IPercentuaisService, PercentuaisService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IArquivoDesenhoService, ArquivoDesenhoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddHttpClient<IFreteDesenhoService, FreteDesenhoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationHandler>();

            services.AddSingleton<IPdf, Pdf>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}

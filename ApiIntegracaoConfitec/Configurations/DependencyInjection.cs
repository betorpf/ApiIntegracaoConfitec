using ApiIntegracaoConfitec.Business.Confitec;
using ApiIntegracaoConfitec.Business.Sompo;
using ApiIntegracaoConfitec.Domain.Handler;
using ApiIntegracaoConfitec.Infrastructure.Connection;
using ApiIntegracaoConfitec.Infrastructure.Repository;
using ApiIntegracaoConfitec.Interfaces.Business.Confitec;
using ApiIntegracaoConfitec.Interfaces.Business.Sompo;
using ApiIntegracaoConfitec.Interfaces.Domain.Handler;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Connection;
using ApiIntegracaoConfitec.Interfaces.Infrastructure.Repository;
using ApiIntegracaoConfitec.Interfaces.Service;
using ApiIntegracaoConfitec.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApiIntegracaoConfitec.Configurations
{
    public static class DependencyInjection
    {
        public static void ConfigurationDependencyInjection(IServiceCollection services)
        {
            #region Dependency Injection
            services.AddScoped<IConfitecService, ConfitecService>();
            services.AddScoped<ISolicitarInspecaoHandler, SolicitarInspecaoHandler>();
            services.AddScoped<IBuscarDadosSolicitarInspecaoHandler, BuscarDadosSolicitarInspecaoHandler>();
            services.AddScoped<ICancelarInspecaoHandler, CancelarInspecaoHandler>();
            services.AddScoped<ISQLConnectionDefault, SqlConnectionDefault>();
            services.AddScoped<ISompoRepository, SompoRepository>();
            services.AddScoped<IEnviarSolicitacaoInspecaoConfitecHandler, EnviarSolicitacaoInspecaoConfitecHandler>();
            services.AddScoped<IBuscarDadosAutenticacaoConfitecHandler, BuscarDadosAutenticacaoConfitecHandler>();
            services.AddScoped<ISolicitarAutenticacaoConfitecHandler, SolicitarAutenticacaoConfitecHandler>();
            services.AddScoped<IGravarRespostaInspecaoHandler, GravarRespostaInspecaoHandler>();
            services.AddScoped<IEnviarRetornoLaudoHandler, EnviarRetornoLaudoHandler>();
            services.AddScoped<IGravarDadosLaudoHandler, GravarDadosLaudoHandler>();
            services.AddScoped<IEnviarSolicitacaoCancelamentoConfitecHandler, EnviarSolicitacaoCancelamentoConfitecHandler>();
            services.AddScoped<IGravarRespostaCancelamentoHandler, GravarRespostaCancelamentoHandler>();
            #endregion
        }
    }
}

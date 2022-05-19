using ApiIntegracaoConfitec.Business.Sompo;
using ApiIntegracaoConfitec.Domain.Handler;
using ApiIntegracaoConfitec.Infrastructure.Connection;
using ApiIntegracaoConfitec.Infrastructure.Repository;
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
            services.AddScoped<IDadosInspecaoSompoRepository, DadosInspecaoSompoRepository>();
            #endregion
        }
    }
}

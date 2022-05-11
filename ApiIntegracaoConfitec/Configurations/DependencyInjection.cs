﻿using ApiIntegracaoConfitec.Business;
using ApiIntegracaoConfitec.Interfaces.Business;
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
            services.AddTransient<IConfitecService, ConfitecService>();
            services.AddTransient<ISompoBusiness, SompoBusiness>();
            services.AddTransient<IConfitecBusiness, ConfitecBusiness>();
            #endregion
        }
    }
}

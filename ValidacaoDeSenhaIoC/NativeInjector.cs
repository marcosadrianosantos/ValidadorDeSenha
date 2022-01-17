using Microsoft.Extensions.DependencyInjection;
using System;
using ValidacaoDeSenha.Application.Interfaces;
using ValidacaoDeSenha.Application.Services;

namespace ValidacaoDeSenhaIoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Service

            services.AddScoped<IValidaSenhaService, ValidaSenhaService>();

            #endregion
        }
    }
}

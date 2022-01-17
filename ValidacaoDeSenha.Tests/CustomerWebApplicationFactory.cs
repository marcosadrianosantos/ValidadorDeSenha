using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using ValidacaoDeSenha.Application.Services;

namespace ValidacaoDeSenha.Tests
{
    public class CustomerWebApplicationFactory<TStartup> : WebApplicationFactory<TestesStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection().BuildServiceProvider();
                var coreService = new ValidaSenhaService();
                services.AddSingleton(coreService);
                services.AddSingleton<IStartupFilter, CustomStartupFilter>();
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
                services.AddMvc(opts => opts.EnableEndpointRouting = false);
                services.AddMvc(opts => { opts.Filters.Add(new AllowAnonymousFilter()); });

            });
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null).UseStartup<TestesStartup>();
        }

    }

    public class CustomStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<EnderecoMiddleware>();
                next(app);
            };
        }
    }
}


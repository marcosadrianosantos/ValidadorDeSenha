using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ValidacaoDeSenha.Application.ViewModel;
using Xunit;

namespace ValidacaoDeSenha.Tests
{
    public class TestesIntegrado : IClassFixture<CustomerWebApplicationFactory<TestesStartup>>
    {
        private readonly HttpClient client;

        public TestesIntegrado(CustomerWebApplicationFactory<TestesStartup> factory)
        {
            client = factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            client.DefaultRequestHeaders.Add("Authorization", "basic");
            client.Timeout = new TimeSpan(0, 1, 0);

        }

        [Fact]
        public async Task ExecutaValidacaoComSucesso()
        {
            var httpResponse = await client.GetAsync("/ValidarSenha/validar/Abcdef@1234&5");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var responseAPI = JsonConvert.DeserializeObject<SenhaResponseViewModel>(stringResponse);
            Assert.True(responseAPI.senhaIsValida == true);

        }

        [Fact]
        public async Task ExecutaValidacaoComErroNaValidacao()
        {
            var httpResponse = await client.GetAsync("/ValidarSenha/validar/AbcdAf@1234&5");

            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var responseAPI = JsonConvert.DeserializeObject<SenhaResponseViewModel>(stringResponse);
            Assert.True(responseAPI.senhaIsValida == false);
        }
    }
}

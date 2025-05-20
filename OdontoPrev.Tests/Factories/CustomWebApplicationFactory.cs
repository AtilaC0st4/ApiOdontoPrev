using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove o HttpClient real
            services.RemoveAll<HttpClient>();

            // Adiciona o HttpClient falso
            services.AddHttpClient("ViaCepClient")
         .ConfigurePrimaryHttpMessageHandler(() => new FakeHttpMessageHandler());

        });
    }
}

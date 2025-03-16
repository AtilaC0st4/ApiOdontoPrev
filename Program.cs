using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using OdontoPrev.Data; // Certifique-se de importar o namespace correto
using OdontoPrev.Models; // Importe o namespace dos modelos
using OdontoPrev;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona serviços ao container.
        builder.Services.AddControllers(); // Adiciona suporte para controllers da API
        builder.Services.AddEndpointsApiExplorer(); // Adiciona suporte para explorar endpoints
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "OdontoPrev",
                Version = "v1",
                Description = "API para rastrear escovações de dentes e calcular pontos.",
                Contact = new OpenApiContact
                {
                    Name = "Átila",
                    Email = "rm552650@fiap.com.br",
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                }
            });

            // Configura o Swagger para usar comentários XML
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        // Configura o DbContext para usar o Oracle
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"));
        });


        // Adiciona o Singleton para AppSettings
        builder.Services.AddSingleton<AppSettings>(new AppSettings
        {
            ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        });

        var app = builder.Build();

        // Configura o pipeline de requisições HTTP.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        // Mapeia os controllers da API
        app.MapControllers();

        app.Run();
    }
}
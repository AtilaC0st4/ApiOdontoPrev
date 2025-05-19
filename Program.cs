using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OdontoPrev.Data;
using OdontoPrev;
using System.Reflection;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona os controllers e configura o JsonSerializerOptions para ignorar ciclos de referência
        builder.Services.AddControllers(options =>
        {
            // Adicione filtros ou outras configurações de controller, se necessário
        }).AddJsonOptions(options =>
        {
            // Ignora ciclos de referência e não usa $id e $ref
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        // Adiciona o explorador de endpoints e a configuração do Swagger
        builder.Services.AddEndpointsApiExplorer();
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

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

        });

        // Configura o contexto de banco de dados com Oracle
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Configura o CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        // Adiciona as configurações do AppSettings
        builder.Services.AddSingleton<AppSettings>(new AppSettings
        {
            ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        });

        builder.Services.AddHttpClient<ViaCepService>();


        var app = builder.Build();

        // Habilita o CORS antes de outras configurações de pipeline
        app.UseCors("AllowAll");

        // Habilita o Swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        // Configura o pipeline de requisições
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // Mapeia os controllers
        app.MapControllers();

        // Roda a aplicação
        app.Run();
    }
}

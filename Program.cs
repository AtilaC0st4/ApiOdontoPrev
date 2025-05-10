using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

using OdontoPrev.Data;
 
using OdontoPrev;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddControllers(); 
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
            c.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"));
        });


        
        builder.Services.AddSingleton<AppSettings>(new AppSettings
        {
            ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        });

        var app = builder.Build();

       
            app.UseSwagger();
            app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        
        app.MapControllers();


        app.Run();
    }
}
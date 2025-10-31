using CP2.Infrastructure.Persistence.Entitites;
using CP2.Infrastructure.Persistence.Repositories;
using CP2.UseCase;
using CP2.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CP2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var swaggerConfig = builder
            .Configuration
            .GetSection("Swagger")
            .Get<SwaggerConfig>();
        
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = swaggerConfig.Title,
                    Version = "v1",
                    Description = swaggerConfig.Description,
                    Contact = swaggerConfig.Contact
                });
                
                swagger.EnableAnnotations();

                foreach (var server in swaggerConfig.Servers)
                {
                    swagger.AddServer(new OpenApiServer
                    {
                        Url   = server.Url,
                        Description = server.Name
                    });
                }
            }
        );
        
        builder.Services.AddDbContext<Cp2Context>(options =>
            options.UseOracle(builder.Configuration.GetConnectionString("OracleDb"))
        );
        
        builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
        builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
        builder.Services.AddScoped<IAlunoUseCase, AlunoUseCase>();
        builder.Services.AddScoped<IProfessorUseCase, ProfessorUseCase>();
        builder.Services.AddScoped<ITurmaUseCase, TurmaUseCase>();
        
        var app = builder.Build();

        // Configuração do pipeline HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
                {
                    ui.SwaggerEndpoint("/swagger/v1/swagger.json", "CP2 DOTNET.API v1");
                    ui.RoutePrefix = string.Empty;
                }            
            );
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();
        app.Run();
    }
}
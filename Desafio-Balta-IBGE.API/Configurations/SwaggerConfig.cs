using Microsoft.OpenApi.Models;

namespace Desafio_Balta_IBGE.API.Configurations;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Desafio IBGE - Balta IO",
                Description = "<h4>API desenvolvida para o desafio do balta.io," +
                "o objetivo deste projeto é fornecer funcionalidades de cadastro de usuários e um conjunto completo de operações CRUD para localidades</h4>",
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                                "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                                "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                                "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }


    public static void UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Ibge v1");
        });

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
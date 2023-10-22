using Desafio_Balta_IBGE.API.Configurations;
using Desafio_Balta_IBGE.API.Endpoints.Locality;
using Desafio_Balta_IBGE.API.Endpoints.Login;
using Desafio_Balta_IBGE.API.Endpoints.Users;
using Desafio_Balta_IBGE.API.Extensions.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddContext(builder.Configuration);
builder.AddDependencies();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Secrets:JwtPrivateKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Administrador", p => p.RequireRole("Administrador"));
});

var app = builder.Build();

app.AddUserRoutes();
app.AddLoginRoutes();
app.AddLocalityRoutes();

app.UseSwaggerConfiguration(app.Environment);

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.Run();

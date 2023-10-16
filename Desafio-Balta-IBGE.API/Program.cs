using Desafio_Balta_IBGE.API.Endpoints.Users;
using Desafio_Balta_IBGE.API.Extensions.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddContext(builder.Configuration);

builder.AddDependencies();

var app = builder.Build();

app.AddUserRoutes();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

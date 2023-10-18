using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_Balta_IBGE.API.Endpoints.Login
{
    public static class LoginEndpoints
    {
        public static void AddLoginRoutes(this WebApplication app)
        {
            app.MapPost("login", async ([FromBody] LoginRequest request,
                                        [FromServices] ILoginHandler handler,
                                        CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                return Results.Ok(response);
            });
        }
    }
}

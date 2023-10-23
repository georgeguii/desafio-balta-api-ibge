using Desafio_Balta_IBGE.Application.Abstractions.Users;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_Balta_IBGE.API.Endpoints.Login
{
    public static class LoginEndpoints
    {
        public static void AddLoginRoutes(this RouteGroupBuilder route)
        {
            var loginRoute = route.MapGroup("auth");

            loginRoute.MapPost("/login", async ([FromBody] LoginRequest request,
                                        [FromServices] ILoginHandler handler,
                                        CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                return Results.Ok(response);
            })
                .Produces(StatusCodes.Status200OK, typeof(LoginSuccessfully))
                .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Realizar login",
                    Description = "Permite realizar login na aplicação, retorna um token de acesso que deve ser configurado para ser utilizado nos endpoints que o solicitem.",
                })
                .WithTags("Auth");
        }
    }
}

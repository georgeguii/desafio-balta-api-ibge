using Desafio_Balta_IBGE.Application.Abstractions;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_Balta_IBGE.API.Endpoints.Users
{
    public static class UserEndpoints
    {
        public static void AddUserRoutes(this WebApplication app)
        {
            app.MapPost("users/create-user", async ([FromBody]CreateUserRequest request, 
                                                    [FromServices] ICreateUserHandler handler,
                                                    CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                return Results.Ok(response);
            });

            app.MapPut("users/activate-user", async ([FromBody] ActivateUserRequest request,
                                                    [FromServices] IActivateUserHandler handler,
                                                    CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    return Results.StatusCode(500);

                return Results.Ok(response);
            });
        }
    }
}

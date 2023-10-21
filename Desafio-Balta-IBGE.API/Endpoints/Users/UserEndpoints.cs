using Desafio_Balta_IBGE.Application.Abstractions.Users;
using Desafio_Balta_IBGE.Application.UseCases.Users.Handler;
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

            app.MapPut("users/update-name", async ([FromBody] UpdateNameUserRequest request,
                                                   [FromServices] IUpdateNameUserHandler handler,
                                                   CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    return Results.StatusCode(500);

                return Results.Ok(response);
            });

            app.MapPut("users/update-email", async ([FromBody] UpdateEmailUserRequest request,
                                                    [FromServices] IUpdateEmailHandler handler,
                                                    CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    return Results.StatusCode(500);

                return Results.Ok(response);
            });

            app.MapPut("users/update-password", async ([FromBody] UpdatePasswordUserRequest request,
                                                       [FromServices] IUpdatePasswordUserHandler handler,
                                                       CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    return Results.StatusCode(500);

                return Results.Ok(response);
            });

            app.MapDelete("users/{id}", async ([FromRoute] int id,
                                               [FromServices] IDeleteUserHandler handler,
                                               CancellationToken cancellationToken) =>
            {
                var request = new DeleteUserRequest(id);
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

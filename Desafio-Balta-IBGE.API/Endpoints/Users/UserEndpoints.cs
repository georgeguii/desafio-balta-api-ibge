using Desafio_Balta_IBGE.Application.Abstractions.Users;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.DTO;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_Balta_IBGE.API.Endpoints.Users
{
    public static class UserEndpoints
    {
        public static void AddUserRoutes(this WebApplication app)
        {
            app.MapPost("users", async ([FromBody]CreateUserRequest request, 
                                                    [FromServices] ICreateUserHandler handler,
                                                    CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                return Results.Created("users/activate-user", response);

            }).Produces(StatusCodes.Status201Created, typeof(CreatedSuccessfully))
              .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Registro de uma nova conta de usuário.",
                  Description = "Endpoint para registrar uma nova conta de usuário. É retornado um código de ativação com prazo de expiração do código de 5 minutos.",
              })
              .WithTags("Users");

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

            }).Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status500InternalServerError)
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Ativação de conta de usuário.",
                  Description = "Endpoint para ativação de conta de usuário.",
              })
              .WithTags("Users");

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

            }).Produces(StatusCodes.Status200OK, typeof(UpdatedSuccessfully))
              .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
              .Produces(StatusCodes.Status500InternalServerError, typeof(UpdateUserError))
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Atualização do nome do usuário.",
                  Description = "Endpoint para atualizar o nome do usuário.",
              })
              .RequireAuthorization("Administrador")
              .WithTags("Users");

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

            }).Produces(StatusCodes.Status200OK, typeof(UpdatedSuccessfully))
              .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
              .Produces(StatusCodes.Status500InternalServerError, typeof(UpdateUserError))
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Atualização do E-mail do usuário.",
                  Description = "Endpoint para atualizar e-mail do usuário.",
              })
              .RequireAuthorization("Administrador")
              .WithTags("Users");

            app.MapPut("users/{id}/update-password", async ([FromRoute] int id,
                                                       [FromBody] UpdatePasswordUserRequest request,
                                                       [FromServices] IUpdatePasswordUserHandler handler,
                                                       CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return Results.BadRequest(response);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                    return Results.StatusCode(500);

                return Results.Ok(response);
            }).Produces(StatusCodes.Status200OK, typeof(UpdatedSuccessfully))
              .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
              .Produces(StatusCodes.Status500InternalServerError, typeof(UpdateUserError))
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Atualização da senha de usuário.",
                  Description = "Endpoint para atualizar senha conta de usuário.",
              })
              .RequireAuthorization("Administrador")
              .WithTags("Users");

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
            }).Produces(StatusCodes.Status200OK, typeof(DeletedSuccessfully))
              .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
              .Produces(StatusCodes.Status500InternalServerError, typeof(DeleteUserError))
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Excluir conta de usuário.",
                  Description = "Endpoint para excluir conta de usuário.",
              })
              .RequireAuthorization("Administrador")
              .WithTags("Users");

            app.MapGet("users", async ([FromServices] IUserQueriesServices services) =>
            {
                var users = await services.GetAllAsync();
                
                return Results.Ok(users);

            })
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Retorna todos os usuários.",
                  Description = "Endpoint para retornar todos os usuários cadastrados.",
              })
              .RequireAuthorization("Administrador")
              .WithTags("Users");
        }
    }
}

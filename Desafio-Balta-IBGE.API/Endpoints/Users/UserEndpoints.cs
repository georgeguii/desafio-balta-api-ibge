using Desafio_Balta_IBGE.Application.Abstractions.Users;
using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using Desafio_Balta_IBGE.Application.UseCases.Users.Response;
using Desafio_Balta_IBGE.Domain.DTO;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Domain.Models;
using Desafio_Balta_IBGE.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_Balta_IBGE.API.Endpoints.Users
{
    public static class UserEndpoints
    {
        public static void AddUserRoutes(this RouteGroupBuilder route)
        {
            var usersRote = route.MapGroup("users");

            usersRote.MapPost("/", async ([FromBody] CreateUserRequest request,
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

            usersRote.MapPut("/activate-user", async ([FromBody] ActivateUserRequest request,
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

            usersRote.MapPut("/update-name", async ([FromBody] UpdateNameUserRequest request,
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

            usersRote.MapPut("/update-email", async ([FromBody] UpdateEmailUserRequest request,
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

            usersRote.MapPut("/{id}/update-password", async ([FromRoute] int id,
                                                       [FromBody] UpdatePasswordUserRequest request,
                                                       [FromServices] IUpdatePasswordUserHandler handler,
                                                       CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(id, request, cancellationToken);

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

            usersRote.MapDelete("/{id}", async ([FromRoute] int id,
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

            usersRote.MapGet("/", async ([FromServices] IUserQueriesServices services) =>
            {
                var users = await services.GetAllAsync();

                return Results.Ok(users);

            }).Produces(StatusCodes.Status200OK, typeof(IEnumerable<UserDTO>))
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Retorna todos os usuários.",
                  Description = "Endpoint para retornar todos os usuários cadastrados.",
              })
              .RequireAuthorization("Administrador")
              .WithTags("Users");

            usersRote.MapGet("/{id}", async ([FromRoute] int id,
                                            [FromServices] IUserQueriesServices services) =>
            {
                var user = await services.GetBydIdAsync(id);

                if (user.StatusCode == HttpStatusCode.NotFound)
                    return Results.NotFound(user.ErrorMessage);

                return Results.Ok(user);

            }).Produces(StatusCodes.Status200OK, typeof(UserDTO))
              .Produces(StatusCodes.Status404NotFound, typeof(UserDTO))
              .WithOpenApi(operation => new(operation)
              {
                  Summary = "Busca um usuário.",
                  Description = "Endpoint para buscar um usuário específico.",
              })
              .RequireAuthorization("Administrador")
              .WithTags("Users");

            usersRote.MapGet("/search-by-email/{email}", async ([FromRoute] string email,
                                                 [FromServices] IUserQueriesServices services,
                                                 CancellationToken cancellationToken) =>
            {
                var user = await services.GetByEmailAsync(email);
                if (user.StatusCode == HttpStatusCode.NotFound)
                    return Results.NotFound(user.ErrorMessage);

                return Results.Ok(user);
            }).Produces(StatusCodes.Status200OK, typeof(UserDTO))
                .Produces(StatusCodes.Status404NotFound, typeof(UserDTO))
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Buscar usuário por email.",
                    Description = "Permite buscar todas o usuário com o email (exato) passado como parâmetro",
                })
                .RequireAuthorization("Administrador")
                .WithTags("Users");

            usersRote.MapGet("/search-by-name/{name}", async ([FromRoute] string name,
                                                              [FromServices] IUserQueriesServices services,
                                                              CancellationToken cancellationToken) =>
            {
                var users = await services.GetByNameAsync(name);
                if (users.StatusCode == HttpStatusCode.NotFound)
                    return Results.NotFound(users.ErrorMessage);

                return Results.Ok(users);
            }).Produces(StatusCodes.Status200OK, typeof(List<UserDTO>))
                .Produces(StatusCodes.Status404NotFound, typeof(List<UserDTO>))
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Buscar localidades por cidade.",
                    Description = "Permite realizar uma busca em todas os usuários com o termo de nome (aproximado) passado como parâmetro",
                })
                .RequireAuthorization("Administrador")
                .WithTags("Users");
        }
    }
}

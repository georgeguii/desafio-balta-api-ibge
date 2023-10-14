using Desafio_Balta_IBGE.Application.UseCases.Users.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio_Balta_IBGE.API.Endpoints.Users
{
    public static class UserEndpoints
    {
        public static void AddUserRoutes(this WebApplication app)
        {
            app.MapPost("create-user", async ([FromBody]CreateUserRequest request, 
                                              [FromServices] ISender sender) =>
            {
                var response = await sender.Send(request);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return Results.BadRequest(response);
                }
                return Results.Ok(response);
            });
        }
    }
}

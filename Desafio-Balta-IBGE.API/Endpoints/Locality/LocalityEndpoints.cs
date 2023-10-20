using System.Net;
using Microsoft.AspNetCore.Mvc;
using Desafio_Balta_IBGE.Application.Abstractions.Locality;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;

namespace Desafio_Balta_IBGE.API.Endpoints.Locality;

public static class LocalityEndpoints
{
    public static void AddLocalityRoutes(this WebApplication app)
    {
        app.MapPost("localities", async ([FromBody] CreateLocalityRequest request,
                                                    [FromServices] ICreateLocalityHandler handler,
                                                    CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(response);

            if (response.StatusCode == HttpStatusCode.Conflict)
                return Results.Conflict(response);

            return Results.Ok(response);
        });

        app.MapDelete("localities", async ([FromBody] DeleteLocalityRequest request,
                                                    [FromServices] IDeleteLocalityHandler handler,
                                                    CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(response);

            if (response.StatusCode == HttpStatusCode.Conflict)
                return Results.NotFound(response);

            return Results.Ok(response);
        });
    }
}

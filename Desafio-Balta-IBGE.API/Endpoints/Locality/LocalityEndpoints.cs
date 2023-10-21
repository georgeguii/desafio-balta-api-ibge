using System.Net;
using Microsoft.AspNetCore.Mvc;
using Desafio_Balta_IBGE.Application.Abstractions.Locality;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Domain.DTO;
using Desafio_Balta_IBGE.Shared.Exceptions;

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

            return Results.Created("localities/{id}", response);
        });

        app.MapPut("localities/{ibgeId}/update-city", async ([FromRoute] string ibgeId,
                                                      [FromBody] UpdateCityLocalityDTO requestDto,
                                                      [FromServices] IUpdateCityLocalityHandler handler,
                                                      CancellationToken cancellationToken) =>
        {
            var request = new UpdateCityLocalityRequest(ibgeId, requestDto.city);
            var response = await handler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(response);

            if (response.StatusCode == HttpStatusCode.Conflict)
                return Results.NotFound(response);

            return Results.Ok(response);
        });

        app.MapPut("localities/{ibgeId}/update-state", async ([FromRoute] string ibgeId,
                                                      [FromBody] UpdateStateLocalityDTO requestDto,
                                                      [FromServices] IUpdateStateLocalityHandler handler,
                                                      CancellationToken cancellationToken) =>
        {
            var request = new UpdateStateLocalityRequest(ibgeId, requestDto.state);
            var response = await handler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(response);

            if (response.StatusCode == HttpStatusCode.Conflict)
                return Results.NotFound(response);

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

        app.MapGet("localities", async ([FromServices] IQueriesServices services,
                                        CancellationToken cancellationToken) =>
        {
            var localities = await services.GetAll();

            return Results.Ok(localities);
        }).RequireAuthorization("Administrador");

        app.MapGet("localities/search-by-state/{state}", async ([FromRoute] string state,
                                                 [FromServices] IQueriesServices services,
                                                 CancellationToken cancellationToken) =>
        {
                var localities = await services.GetLocalitiesByState(state);
                return Results.Ok(localities);
        });

        app.MapGet("localities/search-by-ibgeId/{ibgeId}", async ([FromRoute] string ibgeId,
                                                 [FromServices] IQueriesServices services,
                                                 CancellationToken cancellationToken) =>
        {
            try
            {
                var locality = await services.GetLocalityByIbgeId(ibgeId);
                return Results.Ok(locality);
            }
            catch (NotFoundLocalityException ex)
            {
                return Results.NotFound(ex.Message);
            }

        });


        app.MapGet("localities/search-by-city/{city}", async ([FromRoute] string city,
                                        [FromServices] IQueriesServices services,
                                        CancellationToken cancellationToken) =>
        {
            var locality = await services.GetLocalityByCity(city);
            return Results.Ok(locality);
        });

        
    }
}

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Desafio_Balta_IBGE.Application.Abstractions.Locality;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Request;
using Desafio_Balta_IBGE.Domain.Interfaces.Services;
using Desafio_Balta_IBGE.Domain.DTO;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

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
        })
            .Produces(StatusCodes.Status201Created, typeof(CreatedSuccessfully))
            .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
            .Produces(StatusCodes.Status409Conflict, typeof(CodeAlreadyRegistered))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Criar uma nova localidade",
                Description = "Permite a inserção de uma nova localidade no sistema.",
            })
            .RequireAuthorization("Administrador")
            .WithTags("Localities");

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
        })
            .Produces(StatusCodes.Status200OK, typeof(UpdateCityLocalityResponse))
            .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
            .Produces(StatusCodes.Status404NotFound, typeof(CodeNotFound))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Atualizar a cidade de uma localidade específica",
                Description = "Permite atualizar a cidade de uma localidade existente com base no IBGE ID.",
            })
            .RequireAuthorization("Administrador")
            .WithTags("Localities");

        app.MapPut("localities/{ibgeId}/update-state", async ([FromRoute] string ibgeId,
                                                              [FromBody] UpdateStateLocalityDTO requestDto,
                                                              [FromServices] IUpdateStateLocalityHandler handler,
                                                              CancellationToken cancellationToken) =>
        {
            var request = new UpdateStateLocalityRequest(ibgeId, requestDto.state);
            var response = await handler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(response);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Results.NotFound(response);

            return Results.Ok(response);
        })
            .Produces(StatusCodes.Status200OK, typeof(LocalityDTO))
            .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
            .Produces(StatusCodes.Status404NotFound, typeof(CodeNotFound))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Atualiza o estado de uma localidade específica",
                Description = "Permite atualizar o estado de uma localidade existente com base no IBGE ID.",
            })
            .RequireAuthorization("Administrador")
            .WithTags("Localities");

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
        })
            .Produces(StatusCodes.Status200OK, typeof(DeletedSuccessfully))
            .Produces(StatusCodes.Status400BadRequest, typeof(InvalidRequest))
            .Produces(StatusCodes.Status404NotFound, typeof(CodeNotFound))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Apagar localidade",
                Description = "Permite apagar uma localidade existente com base no IBGE ID.",
            })
            .RequireAuthorization("Administrador")
            .WithTags("Localities");

        app.MapGet("localities", async ([FromQuery] int? page,
                                        [FromQuery] int? pageSize,
                                        [FromServices] IQueriesServices services,
                                        CancellationToken cancellationToken) =>
        {
            var localities = await services.GetAll();

            return Results.Ok(localities);
        })
            .Produces(StatusCodes.Status200OK, typeof(List<LocalityDTO>))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Listar localidades.",
                Description = "Permite listar todas as localidades existentes." +
                "Também é possível utilizar este endpoint de forma páginada, passando os parametros page e pageSize",
            })
            .RequireAuthorization("Administrador")
            .WithTags("Localities");

        app.MapGet("localities/search-by-state/{state}", async ([FromRoute] string state,
                                                 [FromServices] IQueriesServices services,
                                                 CancellationToken cancellationToken) =>
        {
                var localities = await services.GetLocalitiesByState(state);
                return Results.Ok(localities);
        })
            .Produces(StatusCodes.Status200OK, typeof(List<LocalityDTO>))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Buscar localidades por estado.",
                Description = "Permite buscar todas as localidades pertencentes ao Estado (exato) passado como parâmetro",
            })
            .RequireAuthorization("Administrador")
            .WithTags("Localities");

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
                return Results.NotFound(new { Error = ex.Message } );
            }

        })
            .Produces(StatusCodes.Status200OK, typeof(LocalityDTO))
            .Produces(StatusCodes.Status404NotFound, typeof(CodeNotFound))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Buscar localidade pelo código IBGE",
                Description = "Permite buscar uma localidade específica com base no código do IBGE passado como parâmetro",
            })
            .RequireAuthorization("Administrador")
            .WithTags("Localities");


        app.MapGet("localities/search-by-city/{city}", async ([FromRoute] string city,
                                                              [FromServices] IQueriesServices services,
                                                              CancellationToken cancellationToken) =>
        {
            var localities = await services.GetLocalityByCity(city);
            return Results.Ok(localities);
        })
            .Produces(StatusCodes.Status200OK, typeof(List<LocalityDTO>))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Buscar localidades por cidade.",
                Description = "Permite realizar uma busca em todas as localidades com o termo de cidade (aproximado) passado como parâmetro",
            })
            .RequireAuthorization("Administrador")
            .WithTags("Localities");

        
    }
}

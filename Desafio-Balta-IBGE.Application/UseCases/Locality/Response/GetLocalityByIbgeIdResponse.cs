using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Response;
public record GetLocalityByIbgeIdResponse(
    HttpStatusCode StatusCode,
    string IbgeId = null,
    string State = null,
    string City = null,
    string Message = null,
    Dictionary<string, string> Errors = null);
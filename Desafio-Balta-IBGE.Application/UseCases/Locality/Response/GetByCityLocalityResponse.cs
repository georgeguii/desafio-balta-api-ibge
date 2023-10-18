using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

public record GetByCityLocalityResponse (HttpStatusCode StatusCode, string Message, Dictionary<string, string> Errors = null);;
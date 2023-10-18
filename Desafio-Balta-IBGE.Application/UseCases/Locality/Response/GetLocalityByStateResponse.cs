using System.Net;
using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Response;

public record GetLocalityByStateResponse (
    HttpStatusCode StatusCode,
    IEnumerable<Ibge> localities = null,
    string Message = null,
    Dictionary<string, string> Errors = null);

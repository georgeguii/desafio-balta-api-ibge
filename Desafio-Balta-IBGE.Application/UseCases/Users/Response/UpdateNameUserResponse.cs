using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using System.Net;

namespace Desafio_Balta_IBGE.Domain.Models
{
    public record UpdateNameUserResponse(HttpStatusCode StatusCode, string Message, Dictionary<string, string> Errors = null) : IResponse;
}

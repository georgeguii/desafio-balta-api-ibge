using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Response
{
    public record UpdateEmailUserResponse(HttpStatusCode StatusCode, string Message, Dictionary<string, string> Errors = null) : IResponse;
}

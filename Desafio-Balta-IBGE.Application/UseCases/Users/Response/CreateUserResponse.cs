using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using FluentValidation.Results;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Response
{
    public record CreateUserResponse(HttpStatusCode StatusCode, string Message, Dictionary<string, string> Errors) : IResponse;
    
}

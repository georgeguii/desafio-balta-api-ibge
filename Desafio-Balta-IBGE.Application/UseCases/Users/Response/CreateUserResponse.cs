using FluentValidation.Results;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Response
{
    public record CreateUserResponse(HttpStatusCode StatusCode, string Message, List<ValidationFailure> Errors);
    
}

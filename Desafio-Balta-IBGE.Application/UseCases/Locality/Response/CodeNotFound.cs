using System.Net;
using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Response;
public class CodeNotFound : IResponse
{
    public CodeNotFound(HttpStatusCode StatusCode, string Message)
    {
        this.StatusCode = StatusCode;
        this.Message = Message;
    }

    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public Dictionary<string, string> Errors { get; set; }
}

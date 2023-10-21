using System.Net;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Abstractions
{
    public interface IResponse
    {
        HttpStatusCode StatusCode { get; set; }
        string Message { get; set; }
        Dictionary<string, string> Errors { get; set; }
    }
}

using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using System.Net;

namespace Desafio_Balta_IBGE.Application.Abstractions.Response
{
    public class DomainNotification : IResponse
    {
        public DomainNotification(HttpStatusCode StatusCode, List<Dictionary<string, string>> Errors)
        {
            this.StatusCode = StatusCode;
            this.Errors = Errors
                         .SelectMany(dict => dict)
                         .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }
}

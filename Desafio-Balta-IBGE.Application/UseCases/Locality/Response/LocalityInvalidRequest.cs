using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Locality.Response
{
    public class LocalityInvalidRequest : IResponse
    {
        public LocalityInvalidRequest(HttpStatusCode StatusCode, string Message, Dictionary<string, string> Errors)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.Errors = Errors;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }
}

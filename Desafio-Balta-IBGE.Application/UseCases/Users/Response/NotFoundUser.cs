using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Response
{
    public class NotFoundUser : IResponse
    {
        public NotFoundUser(HttpStatusCode StatusCode, string Message)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }
}

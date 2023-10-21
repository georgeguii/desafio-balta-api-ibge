using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Response
{
    public class LoginSuccessfully : IResponse
    {
        public LoginSuccessfully(HttpStatusCode StatusCode, string Message, string token)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            Token = token;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Token { get; }
        public Dictionary<string, string> Errors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

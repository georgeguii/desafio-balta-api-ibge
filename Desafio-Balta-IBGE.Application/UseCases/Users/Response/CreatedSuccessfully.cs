using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using System.Net;

namespace Desafio_Balta_IBGE.Application.UseCases.Users.Response
{
    public class CreatedSuccessfully : IResponse
    {
        public CreatedSuccessfully(HttpStatusCode StatusCode, string Message, string ActivationCode, string ExpireDate)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.ActivationCode = ActivationCode;
            this.ExpireDate = ExpireDate;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string ActivationCode { get; set; }
        public string ExpireDate { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }
    
}

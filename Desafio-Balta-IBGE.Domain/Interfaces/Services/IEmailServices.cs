using Desafio_Balta_IBGE.Domain.Models;

namespace Desafio_Balta_IBGE.Domain.Interfaces.Services
{
    public interface IEmailServices
    {
        Task<bool> SendVerificationMail(User user);
        Task SendActivationSuccess(User user);
    }
}

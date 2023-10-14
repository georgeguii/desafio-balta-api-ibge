using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Shared.ValueObjects;

namespace Desafio_Balta_IBGE.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public Email() { }
        public Email(string address)
        {
            InvalidParametersException.ThrowIfNull(address, "Email inválido.");
            Address = address;
        }

        public string Address { get; private set; }
        public VerifyEmail VerifyEmail { get; private set; } = new VerifyEmail();

        public static implicit operator string(Email email) => email.ToString();
        public override string ToString() => Address.Trim();
        public static implicit operator Email(string endereco) => new Email(endereco);
        public void ResendCode()
        {
            VerifyEmail = new VerifyEmail();
            VerifyEmail.GenerateCode();
        }
    }
}

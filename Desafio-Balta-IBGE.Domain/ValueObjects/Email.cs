using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using Desafio_Balta_IBGE.Shared.Atributes;
using Desafio_Balta_IBGE.Shared.Extensions;
using Desafio_Balta_IBGE.Shared.ValueObjects;
using Errors = System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>;

namespace Desafio_Balta_IBGE.Domain.ValueObjects
{
    public sealed class Email : ValueObject, IValidate
    {
        public Email() { }
        public Email(string address)
        {
            Address = address.Trim();
        }

        [IfNull(ErrorMessage = "Email inválido.")]
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

        public void UpdateEmail(string email)
        {
            Address = email;
            Validate();
        }
        public void Validate()
        {
            var errors = new Errors();
            errors.AddRange(this.CheckIfPropertiesIsNull());
            if (errors.Count > 0)
            {
                AddNotification(errors);
            }
        }

    }
}

using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using Desafio_Balta_IBGE.Domain.ValueObjects;
using Desafio_Balta_IBGE.Shared.Atributes;
using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Shared.Extensions;
using Errors = System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>;

namespace Desafio_Balta_IBGE.Domain.Models
{
    public sealed class User : Entity, IValidate
    {
        public User() {}
        public User(string name, Password password, Email email, string role)
        {
            Name = name;
            Password = password;
            Email = email;
            Role = role;
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

        public int UserId { get; private set; }

        [IfNull(ErrorMessage = "Nome inválido.")]
        public string Name { get; private set; }

        [IfNull(ErrorMessage = "Senha inválida.")]
        public Password Password { get; private set; }

        [IfNull(ErrorMessage = "Email inválido.")]
        public Email Email { get; private set; }

        [IfNull(ErrorMessage = "Role inválido.")]
        public string Role { get; private set; }

        public void UpdateName(string name)
        {
            Name = name;
            Validate();
        }
    }
}

using Desafio_Balta_IBGE.Domain.Atributes;
using Desafio_Balta_IBGE.Domain.ValueObjects;
using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Shared.Extensions;

namespace Desafio_Balta_IBGE.Domain.Models
{
    public sealed class User : Entity
    {
        public User() {}
        public User(string name, Password password, Email email, string role)
        {
            Name = name.Trim();
            Password = password;
            Email = email;
            Role = role;
            this.CheckPropertiesIsNull();
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
            InvalidParametersException.ThrowIfNull(name, "Nome inválido.");
            Name = name;
        }

        
    }
}

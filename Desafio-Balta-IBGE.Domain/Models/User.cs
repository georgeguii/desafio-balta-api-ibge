using Desafio_Balta_IBGE.Domain.ValueObjects;
using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Shared.Exceptions;

namespace Desafio_Balta_IBGE.Domain.Models
{
    public sealed class User : Entity
    {
        public User() {}
        public User(string name, Password password, Email email)
        {
            InvalidParametersException.ThrowIfNull(name, "Nome inválido.");
            InvalidParametersException.ThrowIfNull(password, "Senha inválida.");
            InvalidParametersException.ThrowIfNull(email, "Email inválido.");

            Name = name.Trim();
            Password = password;
            Email = email;
        }

        public int UserId { get; private set; }
        public string Name { get; private set; }
        public Password Password { get; private set; }
        public Email Email { get; private set; }

        public void UpdateName(string name)
        {
            InvalidParametersException.ThrowIfNull(name, "Nome inválido.");
            Name = name;
        }

        
    }
}

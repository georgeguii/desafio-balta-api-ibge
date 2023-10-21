using Desafio_Balta_IBGE.Domain.Interfaces.Abstractions;
using Desafio_Balta_IBGE.Shared.Atributes;
using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Shared.Extensions;
using Errors = System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, string>>;

namespace Desafio_Balta_IBGE.Domain.Models;

public sealed class Ibge : Entity, IValidate
{
    [IfNull(ErrorMessage = "Id inválido.")]
    public string IbgeId { get; private set; } = string.Empty;

    [IfNull(ErrorMessage = "Cidade inválida.")]
    public string City { get; private set; } = string.Empty;

    [IfNull(ErrorMessage = "Estado inválido.")]
    public string State { get; private set; } = string.Empty;

    public Ibge()
    {
    }

    public Ibge(string ibgeId ,string city, string state)
    {
        IbgeId = ibgeId;
        City = city;
        State = state;
        Validate();
    }

    public void UpdateCity(string city)
    {
        City = city;
        Validate();
    }

    public void UpdateState(string state)
    {
        State = state;
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

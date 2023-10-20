using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Shared.Exceptions;

namespace Desafio_Balta_IBGE.Domain.Models;

public class Ibge : Entity
{
    public string IbgeId { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;

    public Ibge()
    {
    }

    public Ibge(string ibgeId ,string city, string state)
    {
        IbgeId = ibgeId;
        City = city;
        State = state;
    }

    public void UpdateCity(string city)
    {
        InvalidParametersException.ThrowIfNull(city, "Nome da cidade inválida.");
        City = city;
    }

    public void UpdateState(string state)
    {
        InvalidParametersException.ThrowIfNull(state, "Nome do estado inválido.");
        State = state;
    }
}

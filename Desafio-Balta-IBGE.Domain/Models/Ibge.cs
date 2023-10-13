using Desafio_Balta_IBGE.Shared.Entities;

namespace Desafio_Balta_IBGE.Domain.Models;

public class Ibge : Entity
{
    public string IbgeId { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;

    public Ibge()
    {
    }

    public Ibge(string ibgeId ,string city, string state)
    {
        IbgeId = ibgeId;
        City = city;
        State = state;
    }
}

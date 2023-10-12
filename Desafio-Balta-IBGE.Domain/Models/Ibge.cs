using Desafio_Balta_IBGE.Shared.Entities;

namespace Desafio_Balta_IBGE.Domain.Models;

public class Ibge : Entity
{
    public int IbgeId { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;

    public Ibge()
    {
    }

    public Ibge(string city, string state)
    {
        City = city;
        State = state;
    }
}

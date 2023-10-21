namespace Desafio_Balta_IBGE.Domain.DTO
{
    public class IbgeDTO
    {
        public IbgeDTO(string ibgeId, string city, string state)
        {
            IbgeId = ibgeId;
            City = city;
            State = state;
        }

        public string IbgeId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}

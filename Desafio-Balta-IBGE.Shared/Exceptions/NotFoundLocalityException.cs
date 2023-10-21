namespace Desafio_Balta_IBGE.Shared.Exceptions
{
    public class NotFoundLocalityException : Exception
    {
        public NotFoundLocalityException(string message) : base(message) {}
    }
}

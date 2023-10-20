using System.Reflection;
using System.Runtime.CompilerServices;

namespace Desafio_Balta_IBGE.Shared.Exceptions
{
    public sealed class InvalidParametersException : Exception
    {
        private const string __message = "Dado inválido.";
        public InvalidParametersException(string message = __message) : base(message) { }

        public static void ThrowIfNull(
            string? item,
            string message = __message)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
            {
                throw new InvalidParametersException(message);
            }
        }

        public static void ThrowIfNull(
            string?[] items,
            string message = __message)
        {
            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                {
                    throw new InvalidParametersException(message);
                }
            }
        }

        public static void ThrowIfNull<T>(T obj, string message = __message) where T : class
        {
            if (obj == null)
            {
                throw new InvalidParametersException(message);
            }
        }

        public static void ThrowIfNull<T>(List<T> obj, string message = __message) where T : class
        {
            if (obj == null)
            {
                throw new InvalidParametersException(message);
            }
        }
    }
}

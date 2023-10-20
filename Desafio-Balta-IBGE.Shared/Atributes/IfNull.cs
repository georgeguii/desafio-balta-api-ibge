using System.ComponentModel.DataAnnotations;

namespace Desafio_Balta_IBGE.Domain.Atributes
{
    public class IfNullAttribute : ValidationAttribute
    {
        public string GetErrorMessage()
        {
            return ErrorMessage;
        }
    }
}
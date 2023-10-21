using System.ComponentModel.DataAnnotations;

namespace Desafio_Balta_IBGE.Shared.Atributes
{
    public class IfNullAttribute : ValidationAttribute
    {
        public string GetErrorMessage()
        {
            return ErrorMessage;
        }
    }
}
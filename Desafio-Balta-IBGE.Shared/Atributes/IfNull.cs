using System.ComponentModel.DataAnnotations;

namespace Desafio_Balta_IBGE.Domain.Atributes
{
    public class IfNullAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return false;
            }
            return true;
        }

        public string GetErrorMessage()
        {
            return ErrorMessage;
        }
    }
}
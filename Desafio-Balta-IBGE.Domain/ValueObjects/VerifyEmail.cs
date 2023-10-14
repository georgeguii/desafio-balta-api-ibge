using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Shared.Result;
using Desafio_Balta_IBGE.Shared.ValueObjects;

namespace Desafio_Balta_IBGE.Domain.ValueObjects
{
    public sealed class VerifyEmail : ValueObject
    {
        public string? Code { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public DateTime? ActivateDate { get; private set; } = null;
        public bool Active => ActivateDate != null && ExpireDate == null;

        public void GenerateCode()
        {
            Code = Guid.NewGuid().ToString(format: "N")[0..6].ToUpper();
            ExpireDate = DateTime.Now.AddMinutes(5);
            ActivateDate = null;
        }

        public VerifyCodeResult VerifyCode(string code)
        {
            InvalidParametersException.ThrowIfNull(code, "Código de ativação inválido. Por favor, verifique.");

            if (Code is null && ActivateDate != null)
            {
                return new VerifyCodeResult(IsCodeValid: false, Message: "Essa conta já foi ativada.");
            }

            if (Active)
            {
                return new VerifyCodeResult(IsCodeValid: false, Message: "Este código já foi utilizado.");
            }

            if (ExpireDate < DateTime.Now)
            {
                return new VerifyCodeResult(IsCodeValid: false, Message: "Este código já expirou.");
            }

            if (code.Trim() != Code?.Trim())
            {
                return new VerifyCodeResult(IsCodeValid: false, Message: "Código informado não confere.");
            }

            return new VerifyCodeResult(IsCodeValid: true, Message: string.Empty);
        }

        public void Activate()
        {
            ExpireDate = null;
            Code = null;
            ActivateDate = DateTime.Now;
        }
    }
}

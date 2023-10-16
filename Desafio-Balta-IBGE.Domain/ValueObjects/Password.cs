using Desafio_Balta_IBGE.Infra.Extensions;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Shared.Result;
using Desafio_Balta_IBGE.Shared.ValueObjects;

namespace Desafio_Balta_IBGE.Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        public Password(string hash)
        {
            InvalidParametersException.ThrowIfNull(hash, "Senha inválida.");
            Hash = hash.Trim().Encrypt();
        }

        public string? Hash { get; private set; }
        public string? Code { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public DateTime? ActivateDate { get; private set; } = null;
        public bool Active => ActivateDate != null && ExpireDate == null;

        public bool Verify(string password, string hash)
            => BCrypt.Net.BCrypt.Verify(password, hash);

        public bool Verify(string hash)
            => BCrypt.Net.BCrypt.Verify(hash, Hash);

        public void GenerateCode()
        {
            Code = Guid.NewGuid().ToString("N")[..8].ToUpper();
            ExpireDate = DateTime.Now.AddMinutes(5);
            ActivateDate = null;
        }

        public VerifyCodeResult VerifyCode(string code)
        {
            if (Code is null && ActivateDate != null)
                return new VerifyCodeResult(IsCodeValid: false, Message: "Este código já foi verificado.");

            if (Active)
                return new VerifyCodeResult(IsCodeValid: false, Message: "Este código já foi utilizado.");

            if (code.Trim() != Code?.Trim())
                return new VerifyCodeResult(IsCodeValid: false, Message: "Código informado não confere.");

            if (ExpireDate < DateTime.Now)
                return new VerifyCodeResult(IsCodeValid: false, Message: "Este código já expirou.");

            InvalidParametersException.ThrowIfNull(code, "Código informado é inválido.");

            return new VerifyCodeResult(IsCodeValid: true, Message: string.Empty);
        }
    }
}

namespace Desafio_Balta_IBGE.Infra.Extensions
{
    public static class Criptography
    {
        public static string Encrypt(this string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return senhaCriptografada;
        }

        public static bool CompareHash(this string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}

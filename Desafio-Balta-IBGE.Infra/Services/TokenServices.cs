using Desafio_Balta_IBGE.Domain.Interfaces;
using Desafio_Balta_IBGE.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Desafio_Balta_IBGE.Infra.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration __configuration;
        public string GerarToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(__configuration["Secrets:JwtPrivateKey"]);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GerarClaims(user),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = credentials,
                Issuer = __configuration["Default:Issuer"],
                Audience = __configuration["Default:Audience"]
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        public string GerarToken(IEnumerable<Claim> claims)
        {
            throw new NotImplementedException();
        }

        private ClaimsIdentity GerarClaims(User user)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("Id", user.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Role));

            return claimsIdentity;
        }
    }
}

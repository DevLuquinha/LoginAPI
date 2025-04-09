using LoginAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginAPI.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            // Chave secreta em bytes
            var key = Encoding.UTF8.GetBytes(Configuration.PrivateKey);

            // O tipo de criptografia e a chave para assinar o token
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1), // Define a expiração do token
                Subject = GenerateClaims(user),        // Adiciona os dados do usuário no token
            };

            // Cria uma instância do JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();

            // Gera um token
            var token = handler.CreateToken(tokenDescriptor);

            // Gera a string do token
            var tokenString = handler.WriteToken(token);
            return tokenString;
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            ci.AddClaim(new Claim("Id", user.Id));
            return ci;
        }
    }
}

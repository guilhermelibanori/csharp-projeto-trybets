using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TryBets.Users.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TryBets.Users.Services
{
    public class TokenManager
    {
        private readonly TokenOptions _tokenOptions;
        public TokenManager()
        {
            _tokenOptions = new TokenOptions
            {
                Secret = "4d82a63bbdc67c1e4784ed6587f3730c",
                ExpiresDay = 1
            };
        }

        public string Generate(User user)
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = System.DateTime.UtcNow.AddDays(_tokenOptions.ExpiresDay),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
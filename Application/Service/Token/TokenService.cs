using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Service.Token
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(TokenEmployee tokenEmployee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["JwtSettings:Secret"];

            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("Key não configurada.");

            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, tokenEmployee.Id.ToString()),
                    new Claim(ClaimTypes.Name, tokenEmployee.Name),
                    new Claim(ClaimTypes.Email, tokenEmployee.Email),
                    new Claim(ClaimTypes.Role, tokenEmployee.Role)
                }),

                Expires = DateTime.UtcNow.AddHours(5),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],

                SigningCredentials = new SigningCredentials (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDesciptor);
            return tokenHandler.WriteToken(token);
        }
        
      

    }
}



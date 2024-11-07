using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Contracts.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace Infrastructure.Providers
{
    public class JwtProvider(IOptions<JwtOptions> options): IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;
        public string GenerateToken(User user)
        {
            Claim[] claims = [new(ClaimTypes.Sid, user.Id.ToString())];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}

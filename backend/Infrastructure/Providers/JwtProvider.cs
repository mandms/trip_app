using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Contracts.Utils;

namespace Infrastructure.Providers
{
    public class JwtProvider: IJwtProvider
    {
        public string GenerateToken(User user)
        {
            Claim[] claims = [new("userId", user.Id.ToString())];

            var token = new JwtSecurityToken(
                claims: claims,
                //signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                //    new 
                //    ),
                expires: DateTime.UtcNow.AddHours(1)
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}

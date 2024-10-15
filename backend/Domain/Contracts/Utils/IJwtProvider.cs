using Domain.Entities;

namespace Domain.Contracts.Utils
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
    }
}

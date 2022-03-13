using Domain.Entities;

namespace Domain.Services.Infrastructure
{
    public interface ITokenGenerator
    {
        string GenerateToken(UserEntity user, string issuer, string audience, string key, TimeSpan lifeSpan);
    }
}

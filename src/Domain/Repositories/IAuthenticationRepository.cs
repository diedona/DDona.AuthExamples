using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<UserEntity?> GetAuthorizationUserByUsername(string username);
        Task AddUser(UserEntity newUserEntity);
    }
}

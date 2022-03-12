using Domain.DataTransferObjects.User;

namespace Domain.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<UserAuthorizationDTO?> GetAuthorizationUserByUsername(string username);
    }
}

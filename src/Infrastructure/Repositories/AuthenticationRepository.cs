using Domain.DataTransferObjects.User;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IReadOnlyCollection<UserEntity> _ReadonlyUsers;

        public AuthenticationRepository()
        {
            _ReadonlyUsers = GetSeedUsers();
        }

        public async Task<UserAuthorizationDTO> GetAuthorizationUserByUsername(string username)
        {
            var user = _ReadonlyUsers.FirstOrDefault(x => x.Username == username);
            return await Task.FromResult(new UserAuthorizationDTO());
        }

        private IReadOnlyCollection<UserEntity> GetSeedUsers()
        {
            return new List<UserEntity>
            {
                new UserEntity("diego.dona", "123", new DateTime(1997, 5, 15), "admin", string.Empty),
                new UserEntity("john.castle", "321", new DateTime(1982, 3, 7), "sales", "reports,edit"),
                new UserEntity("naruto", "hokage", new DateTime(2002, 8, 3), "sales", string.Empty)
            };
        }
    }
}

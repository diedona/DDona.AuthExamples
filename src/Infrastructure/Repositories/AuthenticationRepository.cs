using AutoMapper;
using Domain.DataTransferObjects.User;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IReadOnlyCollection<UserEntity> _ReadonlyUsers;
        private readonly IMapper _Mapper;

        private const string DIEGO_PW = "$2a$11$944FPZjio8AG7U6JBjoaB.agxJ2CWP.gW7blMnmvCDwxa6zZshcnS"; //123
        private const string JOHN_PW = "$2a$11$eGNEpyGVPxduJOpcaANzee8rc3HSABdb1qb04nghUYWLfy1m1YZXm"; //321
        private const string NARUTO_PW = "$2a$11$jD873mvzdJPI2h4qivLdkeJuHUEaHulUEGOmMutu0mI4PRMQuFK3y"; //hokage

        public AuthenticationRepository(IMapper mapper)
        {
            _ReadonlyUsers = GetSeedUsers();
            _Mapper = mapper;
        }

        public async Task<UserAuthorizationDTO?> GetAuthorizationUserByUsername(string username)
        {
            var user = _ReadonlyUsers.FirstOrDefault(x => x.Username == username);
            var userDTO = _Mapper.Map<UserAuthorizationDTO>(user);
            return await Task.FromResult(userDTO);
        }

        private IReadOnlyCollection<UserEntity> GetSeedUsers()
        {
            return new List<UserEntity>
            {
                new UserEntity(Guid.NewGuid(), "diego.dona", DIEGO_PW, new DateTime(1997, 5, 15), "admin", string.Empty),
                new UserEntity(Guid.NewGuid(), "john.castle", JOHN_PW, new DateTime(1982, 3, 7), "sales", "reports,edit"),
                new UserEntity(Guid.NewGuid(), "naruto", NARUTO_PW, new DateTime(2002, 8, 3), "sales", string.Empty)
            };
        }
    }
}

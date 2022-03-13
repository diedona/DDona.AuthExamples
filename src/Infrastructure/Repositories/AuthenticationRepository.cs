using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AuthExampleDbContext _Context;
        private readonly DbSet<UserEntity> _DbSet;
        private readonly IMapper _Mapper;
        private readonly IQueryable<UserEntity> _AsNoTracking;

        public AuthenticationRepository(AuthExampleDbContext context, 
            IMapper mapper)
        {
            _Context = context;
            _Mapper = mapper;
            _DbSet = _Context.Set<UserEntity>();
            _AsNoTracking = _Context.Set<UserEntity>().AsNoTracking();
        }

        public async Task<UserEntity?> GetAuthorizationUserByUsername(string username)
        {
            var user = await _AsNoTracking.FirstOrDefaultAsync(x => x.Username.Equals(username));
            return user;
        }
    }
}

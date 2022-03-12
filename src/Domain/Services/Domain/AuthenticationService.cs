using Domain.DataTransferObjects.User;
using Domain.Repositories;
using Domain.Services.Domain.Base;
using Domain.Services.Infrastructure;

namespace Domain.Services.Domain
{
    public class AuthenticationService : BaseService
    {
        private readonly ITokenGenerator _TokenGenerator;
        private readonly IAuthenticationRepository _AuthenticationRepository;

        public AuthenticationService(ITokenGenerator tokenGenerator, 
            IAuthenticationRepository authenticationRepository)
        {
            _TokenGenerator = tokenGenerator;
            _AuthenticationRepository = authenticationRepository;
        }

        public async Task<string?> AuthorizeUser(UserLoginRequestDTO user, string issuer, string audience, string key, TimeSpan lifeSpan)
        {
            var userFromDB = await _AuthenticationRepository.GetAuthorizationUserByUsername(user.Username);
            if (userFromDB == null)
            {
                this.Errors.Add("Invalid credentials");
                return null;
            }

            // should use hashing
            if(!userFromDB.Password.Equals(user.Password))
            {
                this.Errors.Add("Invalid credentials");
                return null;
            }

            //generates token
            return _TokenGenerator.GenerateToken(user, issuer, audience, key, lifeSpan);
        }
    }
}

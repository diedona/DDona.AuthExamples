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
        private readonly IEncryption _Encryption;

        public AuthenticationService(ITokenGenerator tokenGenerator,
            IAuthenticationRepository authenticationRepository, 
            IEncryption encryption)
        {
            _TokenGenerator = tokenGenerator;
            _AuthenticationRepository = authenticationRepository;
            _Encryption = encryption;
        }

        public async Task<string?> AuthorizeUser(UserLoginRequestDTO user, string issuer, string audience, string key, TimeSpan lifeSpan)
        {
            var userEntity = await _AuthenticationRepository.GetAuthorizationUserByUsername(user.Username);
            if (userEntity == null)
            {
                this.Errors.Add("Invalid credentials");
                return null;
            }

            if(!_Encryption.ValidateEquality(user.Password, userEntity.Password))
            {
                this.Errors.Add("Invalid credentials");
                return null;
            }

            return _TokenGenerator.GenerateToken(userEntity, issuer, audience, key, lifeSpan);
        }
    }
}

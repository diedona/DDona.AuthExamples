using Domain.DataTransferObjects.User;
using Domain.Services.Domain.Base;
using Domain.Services.Infrastructure;

namespace Domain.Services.Domain
{
    public class AuthenticationService : BaseService
    {
        private readonly ITokenGenerator _TokenGenerator;

        public AuthenticationService(ITokenGenerator tokenGenerator)
        {
            _TokenGenerator = tokenGenerator;
        }

        public string AuthorizeUser(UserLoginRequestDTO user, string issuer, string audience, string key, TimeSpan lifeSpan)
        {
            // should call repo to validate user

            //generates token
            return _TokenGenerator.GenerateToken(user, issuer, audience, key, lifeSpan);
        }
    }
}

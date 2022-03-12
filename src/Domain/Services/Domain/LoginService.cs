using Domain.DataTransferObjects.User;
using Domain.Services.Domain.Base;
using Domain.Services.Infrastructure;

namespace Domain.Services.Domain
{
    public class LoginService : BaseService
    {
        private readonly ITokenGenerator _TokenGenerator;

        public LoginService(ITokenGenerator tokenGenerator)
        {
            _TokenGenerator = tokenGenerator;
        }

        public string GenerateToken(UserLoginRequestDTO user, string issuer, string key, TimeSpan lifeSpan)
        {
            throw new NotImplementedException();
        }
    }
}

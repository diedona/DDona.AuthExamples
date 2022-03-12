using Domain.DataTransferObjects.User;
using Domain.Services.Domain.Base;

namespace Domain.Services.Domain
{
    public class LoginService : BaseService
    {
        public LoginService()
        {
        }

        public string GenerateToken(UserLoginRequestDTO user, string issuer, string key)
        {
            throw new NotImplementedException();
        }
    }
}

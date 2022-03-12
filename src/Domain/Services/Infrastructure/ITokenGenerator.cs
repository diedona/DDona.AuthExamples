using Domain.DataTransferObjects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Infrastructure
{
    public interface ITokenGenerator
    {
        string GenerateToken(UserLoginRequestDTO user, string issuer, string audience, string key, TimeSpan lifeSpan);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataTransferObjects.User
{
    public class UserLoginRequestDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}

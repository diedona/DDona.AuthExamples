using Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; private set; }
        [JsonIgnore]
        public string Password { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Role { get; private set; }
        public string Claims { get; private set; }

        public UserEntity(string username, string password, DateTime dateOfBirth, string role, string claims) : base()
        {
            Username = username;
            Password = password;
            DateOfBirth = dateOfBirth;
            Role = role;
            Claims = claims;
        }

    }
}

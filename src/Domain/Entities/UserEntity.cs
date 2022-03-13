using Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        private const string ADMIN_ROLE = "admin";
        private readonly IEnumerable<string> _ValidRoles = new[] { "admin", "sales" };

        public string Username { get; private set; }
        [JsonIgnore]
        public string Password { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Role { get; private set; }
        public string? Claims { get; private set; }
        public DateTime? InactivatedAt { get; private set; }

        public UserEntity(Guid id, string username, string password, DateTime dateOfBirth, string role, string? claims) : base(id)
        {
            Username = username;
            Password = password;
            DateOfBirth = dateOfBirth;
            Role = role;
            Claims = claims;
        }

        public bool IsAdmin()
        {
            return Role.Equals(ADMIN_ROLE);
        }

        public void InactivateUser()
        {
            this.InactivatedAt = DateTime.Now;
        }

        public bool IsUserActive()
        {
            return (!this.InactivatedAt.HasValue);
        }

        public bool CanCreateNewUser()
        {
            return this.IsUserActive() && this.IsAdmin();
        }

        public bool HasAnyInvalidRole()
        {
            string[] currentRoles = this.Role.Split(',');
            bool containAnyInvalidRole = currentRoles.Any(c => !_ValidRoles.Contains(c));
            return containAnyInvalidRole;
        }
    }
}

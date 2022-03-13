using Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        private const string ADMIN_ROLE = "admin";
        private readonly IEnumerable<string> _ValidRoles = new[] { "admin", "sales" };

        private UserEntity? _InactivatedByUser;

        public string Username { get; private set; }
        [JsonIgnore]
        public string Password { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Role { get; private set; }
        public string? Claims { get; private set; }
        public DateTime? InactivatedAt { get; private set; }
        public UserEntity? InactivatedByUser
        {
            get => _InactivatedByUser;
            set
            {
                _InactivatedByUser = value;
                InactivatedByUserId = (_InactivatedByUser == null) ? null : _InactivatedByUser.Id;
            }
        }


        public Guid? InactivatedByUserId { get; private set; }

        public virtual ICollection<UserEntity>? UsersInactivatedByMe { get; private set; }

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

        public bool IsActive()
        {
            return (!this.InactivatedAt.HasValue);
        }

        public bool CanCreateNewUser()
        {
            return this.IsActive() && this.IsAdmin();
        }

        public bool HasAnyInvalidRole()
        {
            string[] currentRoles = this.Role.Split(',');
            bool containAnyInvalidRole = currentRoles.Any(c => !_ValidRoles.Contains(c));
            return containAnyInvalidRole;
        }

        public bool CanInactivateAnUser(string usernameToBeInactivated)
        {
            bool sameUsernameAsCurrent = this.Username.Equals(usernameToBeInactivated);
            return this.IsActive() && this.IsAdmin() && (!sameUsernameAsCurrent);
        }

        public void InactivateUser(DateTime dateTimeInactivation, UserEntity issuerUserEntity)
        {
            if (!issuerUserEntity.IsActive() || !issuerUserEntity.IsAdmin() || this.Id == issuerUserEntity.Id)
            {
                _Errors.Add("current user can't do this request");
                return;
            }

            if (!this.IsActive())
            {
                _Errors.Add("can't do this request");
                return;
            }

            this.InactivatedAt = dateTimeInactivation;
            this.InactivatedByUser = issuerUserEntity;
        }

        public void ActivateUser(UserEntity issuerUserEntity)
        {
            if (!issuerUserEntity.IsActive() || !issuerUserEntity.IsAdmin() || this.Id == issuerUserEntity.Id)
            {
                _Errors.Add("current user can't do this request");
                return;
            }

            if (this.IsActive())
            {
                _Errors.Add("can't do this request");
                return;
            }

            this.InactivatedByUser = null;
            this.InactivatedAt = null;
        }
    }
}

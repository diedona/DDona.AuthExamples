using Domain.DataTransferObjects.User;
using Domain.Entities;
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

        public async Task CreateNewUser(string issuerUserName, UserCreateDTO desiredUserToBeCreated)
        {
            var issuerUserEntity = await _AuthenticationRepository.GetAuthorizationUserByUsername(issuerUserName);
            if(issuerUserEntity == null)
            {
                Errors.Add("who the fuck are you?");
                return;
            }

            if(!issuerUserEntity.CanCreateNewUser())
            {
                Errors.Add("current user can't do this request");
                return;
            }

            string hashedPassword = _Encryption.Encrypt(desiredUserToBeCreated.Password);
            UserEntity newUserEntity = new UserEntity(Guid.NewGuid(), 
                desiredUserToBeCreated.Username, hashedPassword,
                desiredUserToBeCreated.DateOfBirth, desiredUserToBeCreated.Role, null);

            if(newUserEntity.HasAnyInvalidRole())
            {
                Errors.Add($"'{newUserEntity.Role}' contain one or more an invalid roles");
                return;
            }

            await _AuthenticationRepository.AddUser(newUserEntity);
        }

        public async Task InactivateUser(string issuerUserName, Guid userId)
        {
            var issuerUserEntity = await _AuthenticationRepository.GetAuthorizationUserByUsername(issuerUserName);
            if (issuerUserEntity == null)
            {
                Errors.Add("who the fuck are you?");
                return;
            }

            var targetUserEntity = await _AuthenticationRepository.GetAuthorizationUserById(userId);
            if(targetUserEntity == null)
            {
                Errors.Add("user not found");
                return;
            }

            targetUserEntity.InactivateUser(DateTime.Now, issuerUserEntity);
            if(targetUserEntity.HasErrors)
            {
                Errors.AddRange(targetUserEntity.GetErrors());
                return;
            }

            await _AuthenticationRepository.UpdateUser(targetUserEntity);
        }

        public async Task ActivateUser(string issuerUserName, Guid userId)
        {
            var issuerUserEntity = await _AuthenticationRepository.GetAuthorizationUserByUsername(issuerUserName);
            if (issuerUserEntity == null)
            {
                Errors.Add("who the fuck are you?");
                return;
            }

            var targetUserEntity = await _AuthenticationRepository.GetAuthorizationUserById(userId);
            if (targetUserEntity == null)
            {
                Errors.Add("user not found");
                return;
            }

            targetUserEntity.ActivateUser(issuerUserEntity);
            if (targetUserEntity.HasErrors)
            {
                Errors.AddRange(targetUserEntity.GetErrors());
                return;
            }

            await _AuthenticationRepository.UpdateUser(targetUserEntity);
        }
    }
}

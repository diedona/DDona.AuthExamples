using Domain.Entities;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations.User
{
    internal static class UserEntityData
    {
        public static IEnumerable<UserEntity> GetSeed()
        {
            var encryption = new BCryptEncryption();
            return new List<UserEntity>()
            {
                new UserEntity(Guid.NewGuid(), "diego.dona", encryption.Encrypt("123"), new DateTime(1999, 02, 14), "admin", null)
            };
        }
    }
}

using Domain.Services.Infrastructure;

namespace Infrastructure.Services
{
    public class BCryptEncryption : IEncryption
    {
        public string Encrypt(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }

        public bool ValidateEquality(string cleanValue, string encryptedValue)
        {
            return BCrypt.Net.BCrypt.Verify(cleanValue, encryptedValue);
        }
    }
}

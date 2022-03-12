namespace Domain.Services.Infrastructure
{
    public interface IEncryption
    {
        string Encrypt(string value);
        bool ValidateEquality(string cleanValue, string encryptedValue);
    }
}

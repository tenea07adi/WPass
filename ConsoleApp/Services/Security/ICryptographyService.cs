
namespace ConsoleApp.Services.Security
{
    public interface ICryptographyService
    {
        public string GenerateIV();
        public string Encrypt(string plainText, string password, string iv);
        public string Encrypt<T>(T obj, string password, string iv);
        public string Decrypt(string encryptedText, string password, string iv);
        public T DecryptObjectM<T>(string encryptedText, string password, string iv);
    }
}

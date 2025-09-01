
namespace ConsoleApp.Services.Security
{
    public interface ICryptographyService
    {
        public string Encrypt(string plainText, string password);
        public string Encrypt<T>(T obj, string password);
        public string Decrypt(string encryptedText, string password);
        public T DecryptObjectM<T>(string encryptedText, string password);
    }
}

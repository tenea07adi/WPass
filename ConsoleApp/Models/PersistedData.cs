
namespace ConsoleApp.Models
{
    public class PersistedData
    {
        public string EncryptedAccounts { get; set; } = string.Empty;
        public int DataModelVersion { get; set; }

        public string IV {  get; set; }= string.Empty;
    }
}


using ConsoleApp.Models.Entity;

namespace ConsoleApp.Models
{
    public class PersistedDataDecrypted
    {
        public List<Account> Accounts { get; set; } = new List<Account>();
        public int DataModelVersion { get; set; }
    }
}

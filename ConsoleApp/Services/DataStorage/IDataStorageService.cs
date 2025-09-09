using ConsoleApp.Models;

namespace ConsoleApp.Services.DataStorage
{
    public interface IDataStorageService
    {
        public string FullFilePath { get; }
        public void InitializeStorage();
        public void WriteData(PersistedDataDecrypted data);
        public PersistedDataDecrypted LoadData();
    }
}

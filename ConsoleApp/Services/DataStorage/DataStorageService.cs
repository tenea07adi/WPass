using ConsoleApp.Models;
using ConsoleApp.Services.Security;

namespace ConsoleApp.Services.DataStorage
{
    public class DataStorageService : IDataStorageService
    {
        private readonly ICryptographyService _cryptographyService;

        private readonly string _filePath;
        private readonly string _fileName;

        public string FullFilePath => Path.Combine(_filePath, _fileName);

        public DataStorageService(string filePath, string fileName, ICryptographyService cryptographyService)
        {
            _filePath = filePath;
            _fileName = fileName;

            _cryptographyService = cryptographyService;

            InitializeStorage();
        }

        public void InitializeStorage()
        {
            CreateFileIfNotExists();
        }

        public PersistedDataDecrypted LoadData()
        {
            var fileData = File.ReadAllText(FullFilePath);

            var data = System.Text.Json.JsonSerializer.Deserialize<PersistedDataDecrypted>(fileData);

            if(data == null)
            {
                throw new Exception("Corrupted file!");
            }

            return data;
        }

        public void WriteData(PersistedDataDecrypted data)
        {
            var fileData = System.Text.Json.JsonSerializer.Serialize(data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FullFilePath, fileData);
        }

        private void CreateFileIfNotExists()
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }

            if (!File.Exists(FullFilePath))
            {
                WriteData(new PersistedDataDecrypted() { Accounts = new List<Account>() });
            }
        }
    }
}

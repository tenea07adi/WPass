using ConsoleApp.Models;
using System.IO;

namespace ConsoleApp.Services.DataStorage
{
    public class DataStorageService : IDataStorageService
    {
        private readonly string _filePath;
        private readonly string _fileName;

        public string FullFilePath => Path.Combine(_filePath, _fileName);

        public DataStorageService(string filePath, string fileName)
        {
            _filePath = filePath;
            _fileName = fileName;

            InitializeStorage();
        }

        public void InitializeStorage()
        {
            CreateFileIfNotExists();
        }

        public PersistedData LoadData()
        {
            var fileData = File.ReadAllText(FullFilePath);

            var data = System.Text.Json.JsonSerializer.Deserialize<PersistedData>(fileData);

            if(data == null)
            {
                throw new Exception("Corrupted file!");
            }

            return data;
        }

        public void WriteData(PersistedData data)
        {
            var fileData = System.Text.Json.JsonSerializer.Serialize(data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FullFilePath, fileData);
        }

        private void CreateFileIfNotExists()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_filePath)))
            {
                Directory.CreateDirectory(_filePath);
            }

            if (!File.Exists(FullFilePath))
            {
                WriteData(new PersistedData() { Accounts = new List<Account>() });
            }
        }
    }
}

using ConsoleApp.Services.DataStorage;
using ConsoleApp.Services.Security;

namespace ConsoleApp
{
    internal class Program
    {
        protected static ICryptographyService? _cryptographyService { get; set; }
        protected static IDataStorageService? _dataStorageService { get; set; }

        static void Main(string[] args)
        {
            InitializeApp();
            StartMenu();
        }

        protected static void StartMenu()
        {
            while (true)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Please type one of the following numbers that corresponds to the option you chose.");
                Console.WriteLine(" ");
                Console.WriteLine("[1] Generate password");
                Console.WriteLine("[2] Add new account");
                Console.WriteLine("[3] Show accounts");

                Console.WriteLine(" ");

                string userInput = Console.ReadLine() ?? "-1";

                Console.WriteLine(" ");

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("This feature is not implemented yet :(");
                        break;

                    case "2":
                        AddAccount();
                        break;

                    case "3":
                        Console.WriteLine("This feature is not implemented yet :(");
                        break;

                    default:
                        Console.WriteLine("Invalid option :(");
                        break;
                }
            }
        }

        protected static void InitializeApp()
        {
            _cryptographyService = new CryptographyService();
            _dataStorageService = new DataStorageService("LocalStorage", "data.json", _cryptographyService);
        }

        private static void AddAccount()
        {
            var data = _dataStorageService!.LoadData();

            Console.WriteLine("Provide a title:");
            string title = Console.ReadLine() ?? " ";

            Console.WriteLine("Provide a link:");
            string url = Console.ReadLine() ?? " ";

            Console.WriteLine("Provide an username:");
            string userName = Console.ReadLine() ?? " ";

            Console.WriteLine("Provide a password:");
            string password = Console.ReadLine() ?? " ";

            Console.WriteLine("Provide details if necessarly:");
            string details = Console.ReadLine() ?? " ";

            DateTime createdOn = DateTime.Now;

            DateTime modifiedOn = DateTime.Now;

            data.Accounts.Add(new Models.Account()
            {
                Title = title,
                Url = url,
                Username = userName,
                Password = password,
                Details = details,
                CreatedOn = createdOn,
                ModifiedOn = modifiedOn
            });

            _dataStorageService.WriteData(data);
        }
    }
}

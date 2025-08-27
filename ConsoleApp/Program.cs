namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                        Console.WriteLine("This feature is not implemented yet :(");
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
    }
}

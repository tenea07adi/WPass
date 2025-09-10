namespace ConsoleApp.Models.Entity
{
    public class Account : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}

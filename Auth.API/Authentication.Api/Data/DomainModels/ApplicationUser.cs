namespace Polls.Api.Data.DomainModels
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
    }
}

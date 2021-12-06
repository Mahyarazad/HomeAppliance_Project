namespace AM.Application.Contracts.Account
{
    public class AccountSearchModel
    {
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long Role { get; set; }
    }
}
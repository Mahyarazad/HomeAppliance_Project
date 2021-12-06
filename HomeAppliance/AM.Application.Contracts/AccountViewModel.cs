using System;

namespace AM.Application.Contracts
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public long Role { get; set; }
        public string CreationTime { get; set; }
    }
}
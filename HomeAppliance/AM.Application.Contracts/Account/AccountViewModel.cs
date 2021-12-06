using System;
using System.Collections.Generic;
using AM.Application.Contracts.Role;

namespace AM.Application.Contracts.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public string Role { get; set; }
        public string CreationTime { get; set; }
    }
}
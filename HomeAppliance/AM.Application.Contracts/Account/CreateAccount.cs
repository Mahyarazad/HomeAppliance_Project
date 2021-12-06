using System;
using System.Collections.Generic;
using AM.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AM.Application.Contracts.Account
{
    public class CreateAccount
    {
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public string PictureString { get; set; }
        public int RoleId { get; set; }
        public List<RoleViewModel> RoleList { get; set; }
    }
}

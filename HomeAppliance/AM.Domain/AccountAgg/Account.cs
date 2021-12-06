using System;
using _0_Framework;

namespace AM.Domain
{
    public class Account : BaseEntity<long>
    {
        public Account(string fullName, string userId, string email, string phoneNumber
            , string password, string profilePicture, long roleId)
        {
            FullName = fullName;
            UserId = userId;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            ProfilePicture = profilePicture;
            RoleId = roleId;
        }

        public string FullName { get; private set; }
        public string UserId { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Password { get; private set; }
        public string ProfilePicture { get; private set; }
        public long RoleId { get; private set; }

        public void Edit(string fullName, string userId, string email, string phoneNumber
            , string profilePicture, long roleId)
        {
            FullName = fullName;
            UserId = userId;
            Email = email;
            PhoneNumber = phoneNumber;
            if (!string.IsNullOrWhiteSpace(profilePicture))
                ProfilePicture = profilePicture;
            RoleId = roleId;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}

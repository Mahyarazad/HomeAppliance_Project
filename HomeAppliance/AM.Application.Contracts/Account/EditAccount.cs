using AM.Application.Contracts.Role;
using System.Collections.Generic;

namespace AM.Application.Contracts.Account
{
    public class EditAccount : RegisterAccount
    {
        public long Id { get; set; }
    }
}
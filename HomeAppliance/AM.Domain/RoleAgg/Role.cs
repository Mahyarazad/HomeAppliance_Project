using System;
using System.Collections.Generic;
using _0_Framework;

namespace AM.Domain
{
    public class Role : BaseEntity<int>
    {
        protected Role()
        {

        }
        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Accounts = new List<Account>();
            Permissions = permissions;
        }

        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; }
        public List<Permission> Permissions { get; private set; }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}

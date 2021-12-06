using System;
using System.Collections.Generic;
using _0_Framework;

namespace AM.Domain
{
    public class Role : BaseEntity<int>
    {
        public Role(string name)
        {
            Name = name;
            Accounts = new List<Account>();
        }

        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; }

        public void Edit(string name)
        {
            Name = name;
        }
    }
}

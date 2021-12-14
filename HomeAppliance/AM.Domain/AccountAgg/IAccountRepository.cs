using _0_Framework;
using AM.Application.Contracts.Account;
using System.Collections.Generic;
using _0_Framework.Application;

namespace AM.Domain
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        EditAccount GetDetail(long Id);
        EditAccount GetDetailByUser(string username);
        ChangePassword getDetailforChangePassword(long Id);

    }
}
using _0_Framework;
using AM.Application.Contracts;
using System.Collections.Generic;

namespace AM.Domain
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        EditAccount GetDetail(long Id);
        ChangePassword getDetailforChangePassword(long Id);
    }
}
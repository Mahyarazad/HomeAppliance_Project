using System.Collections.Generic;
using _0_Framework.Application;

namespace AM.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        OperationResult Create(CreateAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        EditAccount GetDetail(long Id);
        ChangePassword getDetailforChangePassword(long Id);
    }
}
using System.Collections.Generic;
using _0_Framework.Application;

namespace AM.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        OperationResult Register(RegisterAccount command);
        OperationResult RegisterUser(RegisterUser command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        EditAccount GetDetail(long Id);
        ChangePassword getDetailforChangePassword(long Id);
        OperationResult Login(EditAccount command);
        void Logout();
    }
}
using System.Collections.Generic;
using AM.Application.Contracts.Account;
using AM.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Accounts.Account
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public AccountSearchModel SearchModel { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        public SelectList RoleList { get; set; }
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        public IndexModel(IAccountApplication accountApplication,
            IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            RoleList = new SelectList(_roleApplication.GetAll(), "Id", "Name");
            @ViewData["title"] = "Manage Accounts";
            Accounts = _accountApplication.Search(searchModel);
        }

        public IActionResult OnGetRegister()
        {
            var command = new RegisterAccount
            {
                RoleList = _roleApplication.GetAll()
            };
            @ViewData["title"] = "Register a new Account";
            return Partial("./Register", command);
        }

        public JsonResult OnPostRegister(RegisterAccount command)
        {
            var result = _accountApplication.Register(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(long id)
        {
            @ViewData["title"] = "Account Management";
            var account = _accountApplication.GetDetail(id);
            account.RoleList = _roleApplication.GetAll();
            return Partial("./Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            if (!ModelState.IsValid)
            {

            }
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var account = _accountApplication.getDetailforChangePassword(id);
            return Partial("./ChangePassword", account);
        }

        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            if (!ModelState.IsValid)
            {

            }
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }


    }
}

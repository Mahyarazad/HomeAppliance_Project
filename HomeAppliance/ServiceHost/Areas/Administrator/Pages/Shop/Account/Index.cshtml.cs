using System.Collections.Generic;
using System.Linq;
using AM.Application.Contracts;
using IM.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts;
using SM.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Account
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public AccountSearchModel SearchModel { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        public SelectList RoleList { get; set; }
        private readonly IAccountApplication _accountApplication;
        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            // RoleList = new SelectList(.GetList(), "Id", "Name");
            @ViewData["title"] = "Manage Accounts";
            Accounts = _accountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {

            @ViewData["title"] = "Create a new Account";
            return Partial("./Create");
        }

        public JsonResult OnPostCreate(CreateAccount command)
        {
            var result = _accountApplication.Create(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(long id)
        {
            @ViewData["title"] = "Account Management";
            var account = _accountApplication.GetDetail(id);
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

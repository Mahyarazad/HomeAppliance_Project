using System.Collections.Generic;
using AM.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Accounts.Role
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            @ViewData["title"] = "Manage Roles";
            Roles = _roleApplication.GetAll();
        }

        public IActionResult OnGetCreate()
        {
            @ViewData["title"] = "Create a new Account";
            return Partial("./Create");
        }

        public JsonResult OnPostCreate(CreateRole command)
        {
            var result = _roleApplication.Create(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Account Management";
            var role = _roleApplication.GetRole(id);
            return Partial("./Edit", role);
        }

        public JsonResult OnPostEdit(EditRole command)
        {
            if (!ModelState.IsValid)
            {

            }
            var result = _roleApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}

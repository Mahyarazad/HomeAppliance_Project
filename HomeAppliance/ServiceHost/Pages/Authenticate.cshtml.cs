using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework;
using _0_Framework.Application;
using AM.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class Authenticate : PageModel
    {
        [TempData] public string LoginMessage { get; set; }
        [TempData] public string RegisterMessage { get; set; }
        [TempData] public string RegisterSuccess { get; set; }
        private readonly IAccountApplication _accountApplication;
        public Authenticate(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(EditAccount command)
        {
            var result = _accountApplication.Login(command);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            LoginMessage = result.Message;
            return RedirectToPage("./Authenticate");

        }

        public IActionResult OnPostRegisterUser(RegisterUser command)
        {
            var result = _accountApplication.RegisterUser(command);
            if (result.IsSucceeded)
            {
                RegisterSuccess = ApplicationMessage.SuccessfulRegister;
                return RedirectToPage("./Authenticate");
            }
            RegisterMessage = result.Message;
            return RedirectToPage("./Authenticate");

        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("./Index");

        }
    }
}

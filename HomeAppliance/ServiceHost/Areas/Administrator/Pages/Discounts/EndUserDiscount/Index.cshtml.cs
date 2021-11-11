using System.Collections.Generic;
using DM.Application.Contracts;
using DM.Domian;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts;
using SM.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Discounts.EndUser
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EndUserSearchModel SearchModel { get; set; }
        public List<EndUserViewModel> Discounts { get; set; }
        public SelectList ProductList { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IEndUserDiscountApplication _endUserDiscountApplication;

        public IndexModel(IProductApplication productApplication,
            IEndUserDiscountApplication endUserDiscountApplication)
        {
            _productApplication = productApplication;
            _endUserDiscountApplication = endUserDiscountApplication;
        }

        public void OnGet(EndUserSearchModel searchModel)
        {
            ProductList = new SelectList(_productApplication.GetList(), "Id", "Name");
            @ViewData["title"] = "Manage Product";
            Discounts = _endUserDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineEndUserDiscount();
            command.Products = _productApplication.GetList();
            @ViewData["title"] = "Define a new discount";
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineEndUserDiscount command)
        {
            var result = _endUserDiscountApplication.Create(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Discount Management";
            var discount = _endUserDiscountApplication.GetDetails(id);
            discount.Products = _productApplication.GetList();
            return Partial("./Edit", discount);
        }

        public JsonResult OnPostEdit(EditEndUserDiscount command)
        {
            var result = _endUserDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}

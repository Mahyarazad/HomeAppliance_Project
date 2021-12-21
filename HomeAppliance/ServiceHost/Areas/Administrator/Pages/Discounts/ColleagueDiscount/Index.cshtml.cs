using System.Collections.Generic;
using DM.Application.Contracts.Colleague;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Discounts.Colleague
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ColleagueSearchModel SearchModel { get; set; }
        public List<ColleagueViewModel> Discounts { get; set; }
        public SelectList ProductList { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication,
            IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }

        public void OnGet(ColleagueSearchModel searchModel)
        {
            ProductList = new SelectList(_productApplication.GetList(), "Id", "Name");
            @ViewData["title"] = "Manage Collegue discounts";
            Discounts = _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount();
            command.Products = _productApplication.GetList();
            @ViewData["title"] = "Define a new discount";
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Create(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Discount Management";
            var discount = _colleagueDiscountApplication.GetDetails(id);
            discount.Products = _productApplication.GetList();
            return Partial("./Edit", discount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}

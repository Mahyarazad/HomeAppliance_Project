using System.Collections.Generic;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SM.Application.Contracts;
using SM.Infrastructure.Core;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductCategorySearchModel SearchModel { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }
        [RequirePermission(ShopPermissions.SearchProductCategory)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            @ViewData["title"] = "Manage Product Category";
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }
        [RequirePermission(ShopPermissions.CreateProductCategory)]
        public IActionResult OnGetCreate()
        {
            @ViewData["title"] = "Register a Product Category";
            return Partial("./Create", new CreateProductCategory());
        }
        [RequirePermission(ShopPermissions.CreateProductCategory)]
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);

        }
        [RequirePermission(ShopPermissions.EditProductCategory)]
        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Product Category Management";
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("./Edit", productCategory);
        }
        [RequirePermission(ShopPermissions.EditProductCategory)]
        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
        [RequirePermission(ShopPermissions.Deactivate)]
        public JsonResult OnPostDeactive(int Id)
        {
            var result = _productCategoryApplication.Deactivate(Id);
            return new JsonResult(result);
        }
        [RequirePermission(ShopPermissions.Deactivate)]
        public IActionResult OnPostReactive(int Id)
        {
            var result = _productCategoryApplication.Reactivate(Id);
            return new JsonResult(result);
        }
    }
}

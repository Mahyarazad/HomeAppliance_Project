using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SM.Application.Contracts;

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

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            @ViewData["title"] = "Manage Product Category";
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            @ViewData["title"] = "Create a Product Category";
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Product Category Management";
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("./Edit", productCategory);
        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
        public JsonResult OnPostDeactive(int Id)
        {
            var result = _productCategoryApplication.Deactivate(Id);
            return new JsonResult(result);
        }
        public IActionResult OnPostReactive(int Id)
        {
            var result = _productCategoryApplication.Reactivate(Id);
            return new JsonResult(result);
        }
    }
}

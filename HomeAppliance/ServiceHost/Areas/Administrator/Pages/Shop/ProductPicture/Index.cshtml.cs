using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts.Product;
using SM.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductPicture
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductPictureSearchModel SearchModel { get; set; }
        public List<ProductPictureViewModel> ProductPictures { get; set; }
        public SelectList ProductList { get; set; }
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductApplication productApplication,
            IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductList = new SelectList(_productApplication.GetList(), "Id", "Name");
            @ViewData["title"] = "Manage Product Picture";
            ProductPictures = _productPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture();
            command.Products = _productApplication.GetList();
            @ViewData["title"] = "Add a new Picture";
            return Partial("./Register", command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            command.Products = _productApplication.GetList();
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Product Picture Management";
            var productPicture = _productPictureApplication.GetDetails(id);
            productPicture.Products = _productApplication.GetList();
            return Partial("./Edit", productPicture);
        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            command.Products = _productApplication.GetList();
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetDelete(int Id)
        {
            var result = _productPictureApplication.Delete(Id);
            if (result.IsSucceeded)
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
        }
        public IActionResult OnGetReactivate(int Id)
        {
            var result = _productPictureApplication.Reactivate(Id);
            if (result.IsSucceeded)
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");
            }
        }
    }
}

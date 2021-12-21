using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using IM.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts;
using SM.Application.Contracts.Product;
using SM.Infrastructure.Core;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<InventoryViewModel> Inventory { get; set; }
        public SelectList CategoryList { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        private readonly IInventoryApplication _inventoryApplication;
        public IndexModel(IProductApplication productApplication,
            IInventoryApplication inventoryApplication,
            IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
            _inventoryApplication = inventoryApplication;
        }
        [RequirePermission(ShopPermissions.SearchProduct)]
        public void OnGet(ProductSearchModel searchModel)
        {
            CategoryList = new SelectList(_productCategoryApplication.GetList(), "Id", "Name");
            @ViewData["title"] = "Manage Product";
            Products = _productApplication.Search(searchModel);
            Inventory = _inventoryApplication.GetList();
            foreach (var item in Inventory)
            {
                if (item.IsInStock)
                {
                    Products.FirstOrDefault(x => x.Id == item.ProductId).UnitPrice = item.UnitPrice;
                    Products.FirstOrDefault(x => x.Id == item.ProductId).IsInStock = item.IsInStock;
                }
            }
        }
        [RequirePermission(ShopPermissions.CreateProduct)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct();

            command.Categories = _productCategoryApplication.GetList();
            @ViewData["title"] = "Register a Product Category";
            return Partial("./Create", command);
        }
        [RequirePermission(ShopPermissions.CreateProduct)]
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var Categories = _productCategoryApplication.GetList();
            command.CategoryName = Categories.FirstOrDefault(x => x.Id == command.CategoryId).Name;
            var result = _productApplication.Create(command);
            return new JsonResult(result);

        }
        [RequirePermission(ShopPermissions.EditProduct)]
        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Product Category Management";
            var product = _productApplication.GetDetails(id);
            product.Categories = _productCategoryApplication.GetList();
            return Partial("./Edit", product);
        }
        [RequirePermission(ShopPermissions.EditProduct)]
        public JsonResult OnPostEdit(EditProduct command)
        {
            if (!ModelState.IsValid)
            {

            }
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}

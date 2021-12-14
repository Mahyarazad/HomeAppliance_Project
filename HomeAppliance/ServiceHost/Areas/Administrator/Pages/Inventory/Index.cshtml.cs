using System.Collections.Generic;
using _0_Framework.Application;
using DM.Application.Contracts;
using IM.Application.Contracts;
using IM.Infrustructure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts.Product;

namespace ServiceHost.Areas.Administrator.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<InventoryViewModel> Inventory { get; set; }
        public InventorySearchModel SearchModel { get; set; }
        public List<EndUserViewModel> Discounts { get; set; }
        public SelectList ProductList { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication,
            IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }
        [RequirePermission(InventoryPermissions.Search)]
        public void OnGet(InventorySearchModel searchModel)
        {
            ProductList = new SelectList(_productApplication.GetList(), "Id", "Name");
            @ViewData["title"] = "Manage Inventory";
            Inventory = _inventoryApplication.Search(searchModel);
        }
        [RequirePermission(InventoryPermissions.Create)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory();
            command.ProductList = _productApplication.GetList();
            @ViewData["title"] = "Register a new Inventory";

            return Partial("./Register", command);
        }
        [RequirePermission(InventoryPermissions.Create)]
        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);

        }
        [RequirePermission(InventoryPermissions.Edit)]
        public IActionResult OnGetEdit(int id)
        {
            @ViewData["title"] = "Inventory Management";
            var inventory = _inventoryApplication.GetDetail(id);
            inventory.ProductList = _productApplication.GetList();
            return Partial("./Edit", inventory);
        }
        [RequirePermission(InventoryPermissions.Edit)]
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }
        [RequirePermission(InventoryPermissions.Incremenet)]
        public IActionResult OnGetIncrement(int id)
        {
            var inventory = new IncrementInventory()
            {
                InventoryId = id
            };
            return Partial("./Increment", inventory);
        }
        [RequirePermission(InventoryPermissions.Incremenet)]
        public JsonResult OnPostIncrement(IncrementInventory command)
        {
            var result = _inventoryApplication.Increment(command);
            return new JsonResult(result);
        }
        [RequirePermission(InventoryPermissions.Decrement)]
        public IActionResult OnGetDecrement(int Id)
        {
            var inventory = new DecrementInventory()
            {
                InventoryId = Id
            };
            return Partial("./Decrement", inventory);
        }
        [RequirePermission(InventoryPermissions.Decrement)]
        public JsonResult OnPostDecrement(DecrementInventory command)
        {
            var result = _inventoryApplication.Decrement(command);
            return new JsonResult(result);
        }
        [RequirePermission(InventoryPermissions.GetOperationsLogs)]
        public IActionResult OnGetLog(int Id)
        {
            var inventory = _inventoryApplication.GetOperationLogs(Id);
            return Partial("./Log", inventory);
        }
    }
}

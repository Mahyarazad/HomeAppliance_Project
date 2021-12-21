using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Inventory;

namespace IM.Presentation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryQuery _inventoryQuery;

        public InventoryController(IInventoryQuery inventoryQuery)
        {
            _inventoryQuery = inventoryQuery;
        }

        [HttpPost]
        public StockStatus OnPost(CheckStock command)
        {
            return _inventoryQuery.CheckStock(command);
        }
        [HttpGet]
        public StockStatus OnGet(CheckStock command)
        {
            return _inventoryQuery.CheckStock(command);
        }
    }
}

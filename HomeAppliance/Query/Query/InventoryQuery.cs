using System.Linq;
using IM.Domain;
using IM.Infrastructure;
using Query.Contracts.Inventory;
using SM.Infrastructure;

namespace Query.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly IMContext _imContext;
        private readonly SMContext _smContext;

        public InventoryQuery(IMContext imContext, SMContext smContext)
        {
            _imContext = imContext;
            _smContext = smContext;
        }

        public StockStatus CheckStock(CheckStock command)
        {
            var inventory = _imContext.Inventory
                .Select(x => new { x.ProductId, x.Count })
                .FirstOrDefault(x => x.ProductId == command.ProductId);
            if (inventory == null || command.Count > inventory.Count)
            {
                var product = _smContext.Products
                    .Select(x => new { x.Id, x.Name })
                    .FirstOrDefault(x => x.Id == inventory.ProductId);
                return new StockStatus
                {
                    IsInStock = false,
                    ProductName = product.Name
                };
            }

            return new StockStatus();
        }
    }
}
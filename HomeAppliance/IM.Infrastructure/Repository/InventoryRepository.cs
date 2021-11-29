using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using IM.Application.Contracts;
using IM.Domain;
using SM.Infrastructure;

namespace IM.Infrastructure.Repository
{
    public class InventoryRepository : RepositoryBase<int, Inventory>, IInventoryRepository
    {
        private readonly SMContext _smContext;
        private readonly IMContext _imContext;
        public InventoryRepository(IMContext imContext, SMContext smContext) : base(imContext)
        {
            _imContext = imContext;
            _smContext = smContext;
        }

        public EditInventory GetDetails(int Id)
        {
            var inventoryItem = _imContext.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            })
                .FirstOrDefault(x => x.Id == Id);
            return inventoryItem;
        }

        public Inventory GetBy(int ProductId)
        {
            return _imContext.Inventory.FirstOrDefault(x => x.ProductId == ProductId);
        }

        public List<InventoryOperation> GetLog(int Id)
        {
            return _imContext.Inventory.FirstOrDefault(x => x.Id == Id).InventoryOperations.ToList();
        }

        public List<InventoryViewModel> Search(InventorySearchModel search)
        {
            var products = _smContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _imContext.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                IsInStock = x.IsInStock,
                ProductId = x.ProductId,
                CurrentCount = x.CalculateCurrentStock()
            });
            if (search.ProductId > 0)
                query = query.Where(x => x.ProductId == search.ProductId);
            if (search.IsInStock)
                query = query.Where(x => !x.IsInStock);
            var invetory = query.OrderByDescending(x => x.Id).ToList();

            invetory.ForEach(item =>
            {
                item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            });

            return invetory;
        }
    }
}
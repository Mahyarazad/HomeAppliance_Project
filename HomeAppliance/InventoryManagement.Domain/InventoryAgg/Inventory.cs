using System.Collections.Generic;
using System.Linq;
using _0_Framework;

namespace IM.Domain
{
    public class Inventory : BaseEntity<int>
    {
        protected Inventory()
        {
        }
        public Inventory(int productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }

        public void Edit(int productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public int ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public long Count { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        public long CalculateCurrentStock()
        {
            var incoming = InventoryOperations.Where(x => x.OperationType).Sum(x => x.Count);
            var outgoing = InventoryOperations.Where(x => !x.OperationType).Sum(x => x.Count);
            return incoming - outgoing;
        }

        public void Increment(bool operation, string description, long count, long orderId, long operatorId)
        {
            var currentStock = CalculateCurrentStock() + count;
            var inventoryOperation = new InventoryOperation(true, Id, currentStock, count, description, 0, 0);
            InventoryOperations.Add(inventoryOperation);
            Count = currentStock;
            IsInStock = CalculateCurrentStock() > 0;
        }

        public void Decrement(bool operation, string description, long count, long orderId, long operatorId)
        {
            var currentStock = CalculateCurrentStock() - count;
            var inventoryOperation = new InventoryOperation(false, Id, currentStock, count
                , description, orderId, operatorId);
            InventoryOperations.Add(inventoryOperation);
            Count = currentStock;
            IsInStock = CalculateCurrentStock() > 0;
        }
    }
}

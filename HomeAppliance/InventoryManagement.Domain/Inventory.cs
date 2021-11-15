using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework;

namespace InventoryManagement.Domain
{
    public class Inventory : BaseEntity
    {
        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }

        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public long Count { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        private long CalculateCurrentStock()
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
        }

        public void Decrement(bool operation, string description, long count, long orderId, long operatorId)
        {
            var currentStock = CalculateCurrentStock() - count;
            var inventoryOperation = new InventoryOperation(false, Id, currentStock, count
                , description, orderId, operatorId);
            InventoryOperations.Add(inventoryOperation);
        }
    }

    public class InventoryOperation
    {
        public InventoryOperation(bool operationType, long inventoryId, long currentStock, long count
            , string description, long orderId, long operatorId)
        {
            OperationType = operationType;
            InventoryId = inventoryId;
            CurrentStock = currentStock;
            Count = count;
            Description = description;
            OrderId = orderId;
            OperatorId = operatorId;
        }

        public int Id { get; private set; }
        public bool OperationType { get; private set; }
        public long InventoryId { get; private set; }
        public long CurrentStock { get; private set; }
        public DateTime OperationTime { get; private set; }
        public long Count { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long OperatorId { get; private set; }
        public Inventory Inventory { get; private set; }

    }
}

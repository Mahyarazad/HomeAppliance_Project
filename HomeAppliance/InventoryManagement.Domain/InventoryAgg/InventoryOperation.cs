using System;

namespace IM.Domain
{
    public class InventoryOperation
    {
        protected InventoryOperation()
        {
        }
        public InventoryOperation(bool operationType, int inventoryId, long currentStock, long count
            , string description, long orderId, long operatorId)
        {
            OperationType = operationType;
            InventoryId = inventoryId;
            CurrentStock = currentStock;
            Count = count;
            Description = description;
            OrderId = orderId;
            OperatorId = operatorId;
            OperationTime = DateTime.Now;
        }

        public int Id { get; private set; }
        public bool OperationType { get; private set; }
        public int InventoryId { get; private set; }
        public long CurrentStock { get; private set; }
        public DateTime OperationTime { get; private set; }
        public long Count { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long OperatorId { get; private set; }
        public Inventory Inventory { get; private set; }

    }
}
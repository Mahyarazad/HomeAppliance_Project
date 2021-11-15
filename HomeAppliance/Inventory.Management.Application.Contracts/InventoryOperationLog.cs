using System;

namespace Inventory.Management.Application.Contracts
{
    public class InventoryOperationLog
    {
        public bool OperationType { get; set; }
        public long InventoryId { get; set; }
        public long CurrentStock { get; set; }
        public DateTime OperationTime { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }
        public long OrderId { get; set; }
        public long OperatorId { get; set; }
    }
}
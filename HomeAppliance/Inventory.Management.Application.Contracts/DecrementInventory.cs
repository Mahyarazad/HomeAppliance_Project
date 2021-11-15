namespace Inventory.Management.Application.Contracts
{
    public class DecrementInventory
    {
        public long InventoryId { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }
        public string OrderId { get; set; }
        public string OperatorId { get; set; }
    }
}
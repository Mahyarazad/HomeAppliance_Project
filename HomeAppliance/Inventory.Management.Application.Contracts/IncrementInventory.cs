namespace Inventory.Management.Application.Contracts
{
    public class IncrementInventory
    {
        public long InventoryId { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }
    }
}
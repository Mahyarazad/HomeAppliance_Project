namespace Inventory.Management.Application.Contracts
{
    public class InventorySearchModel
    {
        public long ProductId { get; set; }
        public bool IsInStock { get; set; }
    }
}
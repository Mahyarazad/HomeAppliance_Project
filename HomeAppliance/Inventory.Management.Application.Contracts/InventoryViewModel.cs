namespace IM.Application.Contracts
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public bool IsInStock { get; set; }
        public long CurrentCount { get; set; }
        public double UnitPrice { get; set; }
    }
}
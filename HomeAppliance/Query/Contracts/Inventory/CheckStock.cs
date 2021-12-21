using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Query.Contracts.Inventory
{
    public class CheckStock
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }

    public class StockStatus
    {
        public bool IsInStock { get; set; }
        public string ProductName { get; set; }

        public StockStatus()
        {
            IsInStock = true;
        }
    }
}

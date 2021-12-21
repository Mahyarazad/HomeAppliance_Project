using System.Collections.Generic;

namespace SM.Application.Contracts.Order
{
    public class PlaceOrder
    {
        public long AcoountId { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountRate { get; set; }
        public double PayAmount { get; set; }
        public List<AddOrderItem> OrderItems { get; set; }
    }

    public class AddOrderItem 
    {
        public long ProductId { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int DicscountRate { get; set; }
    }
}
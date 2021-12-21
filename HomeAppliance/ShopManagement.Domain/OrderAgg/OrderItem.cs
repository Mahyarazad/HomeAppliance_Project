using _0_Framework;

namespace SM.Domain.OrderAgg
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem(long productId, double quantity, double unitPrice, double discountRate)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountRate = discountRate;
        }

        public long ProductId { get; private set; }
        public double Quantity { get; private set; }
        public double UnitPrice { get; private set; }
        public double DiscountRate { get; private set; }
        public long OrderId { get; private set; }
        public Order Order { get; private set; }
    }
}
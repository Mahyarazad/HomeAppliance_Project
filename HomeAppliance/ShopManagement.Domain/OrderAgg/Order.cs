using System.Collections.Generic;
using System.Net.Sockets;
using _0_Framework;

namespace SM.Domain.OrderAgg
{
    public class Order : BaseEntity<long>
    {
        public Order(long acoountId, double totalAmount, double discountAmount, double payAmount)
        {
            AcoountId = acoountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IsPaid = false;
            IsCanceled = false;
        }

        public long AcoountId { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNumber { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public void PaymentSucceeded(long refId)
        {
            IsPaid = true;
            if (refId != 0)
                RefId = refId;
        }
        public void CancelOrder()
        {
            IsCanceled = true;
        }

        public void AddItem(OrderItem item)
        {
            OrderItems.Add(item);
        }

        public void SetIssueTrackingNumber(string number)
        {
            IssueTrackingNumber = number;
        }
    }
}
using System;
using _0_Framework;

namespace DM.Domian
{
    public class EndUserDiscount : BaseEntity<int>
    {
        public EndUserDiscount(int productId, double discountRate
            , DateTime startTime, DateTime endTime, string occasion)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartTime = startTime;
            EndTime = endTime;
            Occasion = occasion;
        }

        public int ProductId { get; private set; }
        public double DiscountRate { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Occasion { get; private set; }

        public void Edit(int productId
            , DateTime startTime, DateTime endTime, string occasion, double discountRate)
        {
            ProductId = productId;
            StartTime = startTime;
            EndTime = endTime;
            Occasion = occasion;
            DiscountRate = discountRate;
        }
    }
}

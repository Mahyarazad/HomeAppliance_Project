using System.Collections.Generic;
using SM.Application.Contracts.Order;

namespace SM.Application.Contracts.Order
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalPriceAfterDiscount { get; set; }
    }
}
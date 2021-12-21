
using System;
using _0_Framework.Application;
using SM.Application.Contracts.Order;
using SM.Domain.OrderAgg;

namespace SM.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAutenticateHelper _autenticateHelper;
        public OrderApplication(IOrderRepository orderRepository,
            IAutenticateHelper autenticateHelper)
        {
            _orderRepository = orderRepository;
            _autenticateHelper = autenticateHelper;
        }

        public long PlaceOrder(Cart Cart)
        {
            var accountId = _autenticateHelper.CurrentAccountRole().Id;
            var order = new Order(accountId, Cart.TotalPrice, Cart.TotalDiscount, Cart.TotalPriceAfterDiscount);
            foreach (var item in Cart.CartItems)
            {
                var orderItem = new OrderItem((long)item.Id, item.Count, item.Price, item.DiscountRate);
                order.AddItem(orderItem);
            }
            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

    }
}
using System.Collections.Generic;
using SM.Application.Contracts.Order;

namespace Query.Contracts
{
    public interface ICalculateCart
    {
        Cart CalculateCart(List<CartItem> cartItems);
    }
}
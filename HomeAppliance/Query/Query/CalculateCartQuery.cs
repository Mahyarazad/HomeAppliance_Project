using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using DM.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Contracts;
using SM.Application.Contracts.Order;

namespace Query.Query
{
    public class CalculateCartQuery : ICalculateCart
    {
        private readonly DMContext _dmContext;
        private readonly IAutenticateHelper _autenticateHelper;

        public CalculateCartQuery(DMContext dmContext, IAutenticateHelper autenticateHelper)
        {
            _dmContext = dmContext;
            _autenticateHelper = autenticateHelper;
        }


        public Cart CalculateCart(List<CartItem> cartItems)
        {
            var result = new Cart();
            var userRole = _autenticateHelper.CurrentAccountRole().RoleId;
            result.CartItems = cartItems;
            result.TotalDiscount = 0;
            foreach (var item in cartItems)
            {

                var endUserDiscount = _dmContext.EndUserDiscounts
                    .Select(x => new { x.ProductId, x.DiscountRate })
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ProductId == item.Id);
                var colleagueDiscount = _dmContext.ColleagueDiscounts
                    .Select(x => new { x.ProductId, x.DiscountRate })
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ProductId == item.Id);

                item.DiscountRate = 0;

                if (userRole == AuthorizationRoles.User && endUserDiscount != null)
                {
                    item.DiscountRate = endUserDiscount.DiscountRate;
                    item.TotalDiscount = (item.DiscountRate / 100) * item.TotalCart;
                    item.TotalCartAfterDiscount = item.TotalCart * ((100 - item.DiscountRate) / 100);

                    result.TotalDiscount += (item.DiscountRate / 100) * item.TotalCart;
                }

                if (userRole == AuthorizationRoles.Colleague && colleagueDiscount != null)
                {
                    item.DiscountRate = colleagueDiscount.DiscountRate;
                    item.TotalDiscount += (item.DiscountRate / 100) * item.TotalCart;
                    item.TotalCartAfterDiscount = item.TotalCart * ((100 - item.DiscountRate) / 100);

                    result.TotalDiscount += (item.DiscountRate / 100) * item.TotalCart;
                }
                item.TotalCartAfterDiscount = item.TotalCart * ((100 - item.DiscountRate) / 100);
                result.TotalPrice += item.TotalCart;
                result.TotalPriceAfterDiscount += item.TotalCartAfterDiscount;


            }

            return result;
        }
    }
}
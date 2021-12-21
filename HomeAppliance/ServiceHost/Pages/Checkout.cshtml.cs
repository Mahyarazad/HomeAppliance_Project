using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using Newtonsoft.Json;
using Query.Contracts;
using SM.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public const string CookieName = "Cart-items";

        public CheckoutModel(List<string> slugs, List<CartItem> cartList, ICalculateCart calculateCart)
        {
            Slugs = slugs;
            CartList = cartList;
            _calculateCart = calculateCart;
        }

        public List<string> Slugs { get; set; }
        public List<CartItem> CartList { get; set; }
        public Cart CheckoutCart { get; set; }
        private readonly ICalculateCart _calculateCart;

        public void OnGet()
        {

            CartList = new JavaScriptSerializer()
                .Deserialize<List<CartItem>>(Request.Cookies[CookieName]);

            if (CartList != null)
            {
                foreach (var item in CartList)
                {
                    item.TotalCart = item.Count * item.Price;
                    Slugs.Add(Slugify.GenerateSlug(item.Name));
                }
            }

            CheckoutCart = _calculateCart.CalculateCart(CartList);
        }
    }
}

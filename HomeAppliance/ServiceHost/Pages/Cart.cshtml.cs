using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using SM.Application.Contracts.Order;
using SM.Application.Contracts.Product;
using _0_Framework.Application;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public const string CookieName = "cart-items";

        public CartModel(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public List<CartItem> CartList { get; set; }
        public List<string> Slugs { get; set; }
        private readonly IProductApplication _productApplication;
        public void OnGet()
        {
            Slugs = new List<string>();
            CartList = new JavaScriptSerializer()
                .Deserialize<List<CartItem>>(Request.Cookies[CookieName]);
            // var result = _productApplication.CheckInventory(CartList);
            if (CartList != null)
            {
                foreach (var item in CartList)
                {
                    item.TotalCart = item.Count * item.Price;
                    Slugs.Add(Slugify.GenerateSlug(item.Name));
                }
            }
            _productApplication.CheckInventory(CartList);
        }

        public IActionResult OnGetRemoveFromCart(int Id)
        {
            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                IsEssential = true, //<- there
                Expires = DateTime.Now.AddDays(1),
            };

            CartList = new JavaScriptSerializer()
                .Deserialize<List<CartItem>>(Request.Cookies[CookieName]);
            Response.Cookies.Delete(CookieName);

            var tagertToRemove = CartList.FirstOrDefault(x => x.Id == Id);
            CartList.Remove(tagertToRemove);

            Response.Cookies.Append(CookieName, new JavaScriptSerializer().Serialize(CartList), cookieOptions);
            return RedirectToPage("./Cart");
        }

        public IActionResult OnGetGotoCheckOut()
        {
            CartList = new JavaScriptSerializer()
                .Deserialize<List<CartItem>>(Request.Cookies[CookieName]);
            // var result = _productApplication.CheckInventory(CartList);
            _productApplication.CheckInventory(CartList);
            return RedirectToPage("./Index");
        }
    }
}

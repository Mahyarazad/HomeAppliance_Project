using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Query.Contracts.Product;

namespace ServiceHost.Pages
{
    public class SingleProduct : PageModel
    {
        private readonly IProductQuery _productQuery;
        public ProductQueryModel Product { get; set; }
        public SingleProduct(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string Id)
        {
            Product = _productQuery.GetSingleProduct(Id);
        }
    }
}

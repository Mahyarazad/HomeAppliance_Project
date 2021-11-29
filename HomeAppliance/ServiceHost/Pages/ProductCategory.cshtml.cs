using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Product;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductQuery _productQuery;

        public ProductCategoryModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public List<ProductQueryModel> ProductList { get; set; }


        public void OnGet(string Id)
        {
            ProductList = _productQuery.GetListWithSlug(Id);

            @ViewData["Title"] = ReverseSlugify.Reversing(Id);
        }
    }
}

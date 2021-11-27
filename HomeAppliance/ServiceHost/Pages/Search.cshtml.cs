using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Product;

namespace ServiceHost.Pages
{

    public class SearchModel : PageModel
    {
        public string SearchValue { get; set; }
        private readonly IProductQuery _productQuery;
        public List<ProductQueryModel> Products { get; set; }
        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string search)
        {
            SearchValue = search;
            Products = _productQuery.Search(search);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Product;

namespace ServiceHost.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public ProductViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var products = _productQuery.GetList();
            return View(products);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Query.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var categoryList = _productCategoryQuery.GetList();
            return View(categoryList);
        }
    }
}
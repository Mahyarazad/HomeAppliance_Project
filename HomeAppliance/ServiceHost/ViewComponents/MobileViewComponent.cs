using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Query.Contracts;
using Query.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class MobileViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public List<ProductCategoryQueryModel> CategoryList { get; set; }
        public MobileViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            CategoryList = _productCategoryQuery.GetList();
            return View(CategoryList);
        }
    }
}
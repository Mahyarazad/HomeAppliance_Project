using Microsoft.AspNetCore.Mvc;
using SM.Application.Contracts.Product;
using System.Collections.Generic;

namespace SM.Presentation.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;
        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpGet]
        public List<ProductViewModel> OnGetProducts()
        {
            var res = _productApplication.GetList();
            return res;
        }
    }
}

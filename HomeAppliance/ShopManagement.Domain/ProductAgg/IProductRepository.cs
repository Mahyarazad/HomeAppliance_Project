using System.Collections.Generic;
using _0_Framework;
using SM.Application.Contracts.Product;

namespace SM.Domain.ProductAgg
{
    public interface IProductRepository : IRepositoty<int, Product>
    {
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetail(int id);
    }
}
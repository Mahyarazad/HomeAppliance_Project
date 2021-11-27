using System.Collections.Generic;

namespace Query.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetList();
    }
}
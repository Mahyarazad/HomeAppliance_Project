using System.Collections.Generic;

namespace Query.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetList();
        List<ProductQueryModel> GetListWithSlug(string slug);
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetSingleProduct(string slug);
    }
}
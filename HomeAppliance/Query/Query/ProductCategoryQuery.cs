using System.Collections.Generic;
using System.Linq;
using Query.Contracts.ProductCategory;
using SM.Infrastructure;

namespace Query.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly SMContext _smContext;

        public ProductCategoryQuery(SMContext smContext)
        {
            _smContext = smContext;
        }

        public List<ProductCategoryViewModel> GetList()
        {
            return _smContext.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Description = x.Description,
                Keyword = x.Keyword,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }
    }
}
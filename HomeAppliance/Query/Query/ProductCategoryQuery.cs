using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Product;
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

        public List<ProductCategoryQueryModel> GetList()
        {
            var CategoryList = _smContext.ProductCategories.Include(x => x.Products)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Keyword = x.Keyword,
                    MetaDescription = x.MetaDescription,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    IsActive = x.IsActive,
                    Products = new List<ProductQueryModel>()
                }).AsNoTracking().ToList();
            foreach (var item in CategoryList)
            {
                item.Products = _smContext.Products.Where(x => x.CategoryId == item.Id).Select(x =>
                    new ProductQueryModel
                    {
                        Id = x.Id,
                        Picture = x.Picture,
                        PictureTitle = x.PictureTitle,
                        PictureAlt = x.PictureAlt,
                        ProductName = x.Name,
                        Slug = x.Slug,
                        Description = x.Description
                    }).ToList();
            }

            return CategoryList;
        }
    }
}
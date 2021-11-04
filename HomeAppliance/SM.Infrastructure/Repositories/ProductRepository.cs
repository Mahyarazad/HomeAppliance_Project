using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Product;
using SM.Domain.ProductAgg;

namespace SM.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<int, Product>, IProductRepository
    {
        private readonly SMContext _smContext;

        public ProductRepository(SMContext smContext) : base(smContext)
        {
            _smContext = smContext;
        }


        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _smContext.Products.Include(x => x.Category)
                .Select(x =>
                new ProductViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    IsInStock = x.IsInStock,
                    Category = x.Category.Name,
                    CategoryId = x.Category.Id,
                    UnitPrice = x.UnitPrice,
                    CreationTime = x.CreationTime.ToString()

                });
            if (!string.IsNullOrEmpty(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrEmpty(searchModel.Code))
                query = query.Where(x => x.Name.Contains(searchModel.Code));

            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            return query.OrderByDescending(x => x.Id).ToList();

        }

        public EditProduct GetDetail(int id)
        {
            return _smContext.Products.Include(x => x.Category).Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.Category.Id,
                Code = x.Code,
                Description = x.Description,
                Keyword = x.Keyword,
                Picture = x.Picture,
                Slug = x.Slug,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                UnitPrice = x.UnitPrice,
                ShortDescription = x.ShortDescription,
                MetaDescription = x.MetaDescription
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
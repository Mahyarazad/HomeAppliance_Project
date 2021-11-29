using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain;
using SM.Application.Contracts;

namespace SM.Infrastructure.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<int, ProductCategory>, IProductCategoryRepository
    {
        private readonly SMContext _mbContext;

        public ProductCategoryRepository(SMContext mbContext) : base(mbContext)
        {
            _mbContext = mbContext;
        }


        public EditProductCategory GetDetail(int id)
        {
            return _mbContext.ProductCategories
                .Select(x => new EditProductCategory
                {
                    Id = x.Id,
                    Name = x.Name,
                    Keyword = x.Keyword,
                    Slug = x.Slug,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> GetCustomList()
        {
            return _mbContext.ProductCategories
                .Select(x => new ProductCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }

        public void Deactive(int Id)
        {
            var target = _mbContext.ProductCategories.FirstOrDefault(x => x.Id == Id);
            target.Deactivate();
            _mbContext.SaveChanges();
        }

        public void Reactive(int Id)
        {
            var target = _mbContext.ProductCategories.FirstOrDefault(x => x.Id == Id);
            target.Reactivate();
            _mbContext.SaveChanges();
        }

        public ProductCategory Edit(int Id)
        {
            return _mbContext.ProductCategories.FirstOrDefault(x => x.Id == Id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _mbContext.ProductCategories
                .Select(x => new ProductCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    CreationDate = x.CreationTime.ToString(),
                    ProductCount = x.Products.Count,
                    IsActive = x.IsActive

                });
            if (!string.IsNullOrEmpty(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
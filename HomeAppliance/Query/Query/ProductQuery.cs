using System.Collections.Generic;
using System.Linq;
using DM.Infrastructure;
using IM.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Product;
using SM.Infrastructure;

namespace Query.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly SMContext _smContext;
        private readonly DMContext _dmContext;
        private readonly IMContext _imContext;

        public ProductQuery(SMContext smContext, DMContext dmContext, IMContext imContext)
        {
            _smContext = smContext;
            _dmContext = dmContext;
            _imContext = imContext;
        }

        public List<ProductQueryModel> GetList()
        {
            var productQueryList = _smContext.Products.Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    ProductName = x.Name,
                    Slug = x.Slug,
                    ProductCategory = x.Category.Name,
                    Description = x.Description
                }).AsNoTracking().ToList();

            foreach (var product in productQueryList)
            {

                if (_imContext.Inventory
                        .FirstOrDefault(x => x.ProductId == product.Id && x.IsInStock)
                        ?.UnitPrice != null)
                    product.UnitPrice = _imContext.Inventory
                        .FirstOrDefault(x => x.ProductId == product.Id && x.IsInStock).UnitPrice;
                if (_dmContext.EndUserDiscounts
                    .FirstOrDefault(x => x.ProductId == product.Id)
                    ?.DiscountRate != null)
                {
                    product.Discount = _dmContext.EndUserDiscounts
                        .FirstOrDefault(x => x.ProductId == product.Id).DiscountRate;
                    product.EndDate = _dmContext.EndUserDiscounts
                        .FirstOrDefault(x => x.ProductId == product.Id).EndTime;
                    product.EndDateString = product.EndDate.ToString();
                }

                product.PriceAfterDiscount = product.UnitPrice - (product.UnitPrice * (product.Discount * .01));
                // product.Slug = product.PriceAfterDiscount.ToString("N1", CultureInfo.CreateSpecificCulture("fa-ir"));
            }

            return productQueryList;
        }

        public List<ProductQueryModel> GetListWithSlug(string slug)
        {
            var productQueryList = _smContext.Products
                .Include(x => x.Pictures)
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    ProductName = x.Name,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    ProductCategory = x.Category.Name,
                    Description = x.Description,
                    ProductPuctures = x.Pictures
                }).AsNoTracking().Where(x => x.CategorySlug == slug)
                .ToList();

            foreach (var product in productQueryList)
            {

                if (_imContext.Inventory
                    .FirstOrDefault(x => x.ProductId == product.Id && x.IsInStock)
                    ?.UnitPrice != null)
                    product.UnitPrice = _imContext.Inventory
                        .FirstOrDefault(x => x.ProductId == product.Id && x.IsInStock).UnitPrice;
                if (_dmContext.EndUserDiscounts
                    .FirstOrDefault(x => x.ProductId == product.Id)
                    ?.DiscountRate != null)
                {
                    product.Discount = _dmContext.EndUserDiscounts
                        .FirstOrDefault(x => x.ProductId == product.Id).DiscountRate;
                    product.EndDate = _dmContext.EndUserDiscounts
                        .FirstOrDefault(x => x.ProductId == product.Id).EndTime;
                    product.EndDateString = product.EndDate.ToString();
                }

                product.PriceAfterDiscount = product.UnitPrice - (product.UnitPrice * (product.Discount * .01));
                // product.Slug = product.PriceAfterDiscount.ToString("N1", CultureInfo.CreateSpecificCulture("fa-ir"));
            }

            return productQueryList;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var productQueryList = _smContext.Products.Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    ProductName = x.Name,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    ProductCategory = x.Category.Name,
                    Description = x.Description
                }).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(value))
                productQueryList =
                    productQueryList.Where(x => x.ProductName.Contains(value) || x.Description.Contains(value));
            var Products = productQueryList.OrderByDescending(x => x.Id).ToList();

            foreach (var product in Products)
            {

                if (_imContext.Inventory
                    .FirstOrDefault(x => x.ProductId == product.Id && x.IsInStock)
                    ?.UnitPrice != null)
                    product.UnitPrice = _imContext.Inventory
                        .FirstOrDefault(x => x.ProductId == product.Id && x.IsInStock).UnitPrice;
                if (_dmContext.EndUserDiscounts
                    .FirstOrDefault(x => x.ProductId == product.Id)
                    ?.DiscountRate != null)
                {
                    product.Discount = _dmContext.EndUserDiscounts
                        .FirstOrDefault(x => x.ProductId == product.Id).DiscountRate;
                    product.EndDate = _dmContext.EndUserDiscounts
                        .FirstOrDefault(x => x.ProductId == product.Id).EndTime;
                    product.EndDateString = product.EndDate.ToString();
                }

                product.PriceAfterDiscount = product.UnitPrice - (product.UnitPrice * (product.Discount * .01));
            }

            return Products;
        }

        public ProductQueryModel GetSingleProduct(string slug)
        {
            var product
                = _smContext.Products
                    .Include(x => x.Pictures)
                    .Select(x => new ProductQueryModel
                    {
                        Id = x.Id,
                        Picture = x.Picture,
                        PictureTitle = x.PictureTitle,
                        PictureAlt = x.PictureAlt,
                        ProductName = x.Name,
                        Slug = x.Slug,
                        ProductCode = x.Code,
                        CategorySlug = x.Category.Slug,
                        ProductCategory = x.Category.Name,
                        Description = x.Description,
                        ProductPuctures = x.Pictures,
                    }).FirstOrDefault(x => x.Slug == slug);

            if (_imContext.Inventory
                .FirstOrDefault(x => x.ProductId == product.Id && x.IsInStock)
                ?.UnitPrice != null)
            {
                product.UnitPrice = _imContext.Inventory
                    .FirstOrDefault(x => x.ProductId == product.Id).UnitPrice;
                product.IsInStock = _imContext.Inventory
                    .FirstOrDefault(x => x.ProductId == product.Id).IsInStock;
                product.AvailableStock = _imContext.Inventory
                    .FirstOrDefault(x => x.ProductId == product.Id).Count;
            }
            if (_dmContext.EndUserDiscounts
                .FirstOrDefault(x => x.ProductId == product.Id)
                ?.DiscountRate != null)
            {
                product.Discount = _dmContext.EndUserDiscounts
                    .FirstOrDefault(x => x.ProductId == product.Id).DiscountRate;
                product.EndDate = _dmContext.EndUserDiscounts
                    .FirstOrDefault(x => x.ProductId == product.Id).EndTime;
                product.EndDateString = product.EndDate.ToString();
            }

            product.PriceAfterDiscount = product.UnitPrice - (product.UnitPrice * (product.Discount * .01));
            return product;
        }
    }
}
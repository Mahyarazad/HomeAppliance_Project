using System;
using System.Collections.Generic;
using System.Dynamic;
using SM.Domain.ProductPictureAgg;

namespace Query.Contracts.Product
{
    public class ProductQueryModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public string ProductCode { get; set; }
        public string EndDateString { get; set; }
        public DateTime EndDate { get; set; }
        public double PriceAfterDiscount { get; set; }
        public string ProductCategory { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string Slug { get; set; }
        public string CategorySlug { get; set; }
        public bool IsInStock { get; set; }
        public long AvailableStock { get; set; }
        public List<ProductPicture> ProductPuctures { get; set; }
    }
}
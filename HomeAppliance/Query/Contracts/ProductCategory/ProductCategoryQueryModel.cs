﻿using System.Collections.Generic;
using Query.Contracts.Product;

namespace Query.Contracts.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
        public List<ProductQueryModel> Products { get; set; }
    }
}
using System;
using System.Collections.Generic;
using _0_Framework;
using SM.Domain.ProductAgg;

namespace ShopManagement.Domain
{
    public class ProductCategory : BaseEntity
    {
        protected ProductCategory()
        {

        }

        public bool IsActive { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keyword { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public List<Product> Products { get; private set; }

        public ProductCategory(string name, string description, string picture, string pictureAlt
            , string pictureTitle, string keyword, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keyword = keyword;
            MetaDescription = metaDescription;
            Slug = slug;
            Products = new List<Product>();
            IsActive = true;
        }
        public void Edit(string name, string description, string picture, string pictureAlt
            , string pictureTitle, string keyword, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keyword = keyword;
            MetaDescription = metaDescription;
            Slug = slug;

        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Reactivate()
        {
            IsActive = true;
        }
    }
}

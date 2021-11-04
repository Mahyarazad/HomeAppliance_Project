using _0_Framework;
using ShopManagement.Domain;

namespace SM.Domain.ProductAgg
{
    public class Product : BaseEntity
    {
        public Product(string name, string code, double unitPrice,
            string shortDescription, string description, string picture, string pictureAlt,
            string pictureTitle, int categoryId, string slug, string metaDescription, string keyword)
        {
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            IsInStock = true;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Slug = slug;
            MetaDescription = metaDescription;
            Keyword = keyword;
        }
        public void Edit(string name, string code, double unitPrice,
            string shortDescription, string description, string picture, string pictureAlt,
            string pictureTitle, int categoryId, string slug, string metaDescription, string keyword)
        {
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            IsInStock = true;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            CategoryId = categoryId;
            Slug = slug;
            MetaDescription = metaDescription;
            Keyword = keyword;
        }
        protected Product()
        {

        }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public int CategoryId { get; private set; }
        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public string Keyword { get; private set; }
        public ProductCategory Category { get; private set; }

        public void ReplenishStock()
        {
            this.IsInStock = true;
        }
        public void EmptyStock()
        {
            this.IsInStock = false;
        }
    }
}
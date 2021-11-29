using System.Collections.Generic;
using _0_Framework;
using SM.Domain.ProductAgg;

namespace SM.Domain.ProductPictureAgg
{
    public class ProductPicture : BaseEntity
    {
        public ProductPicture(int productId, string picture, string pictureAlt
            , string pictureTitle)
        {
            ProductId = productId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            IsRemoved = false;
        }

        public int ProductId { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }
        public Product Product { get; private set; }
        protected ProductPicture()
        {

        }

        public void Edit(int productId, string picture, string pictureAlt
            , string pictureTitle)
        {
            ProductId = productId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }

        public void Delete()
        {
            this.IsRemoved = true;
        }
        public void Reactivate()
        {
            this.IsRemoved = false;
        }
    }
}
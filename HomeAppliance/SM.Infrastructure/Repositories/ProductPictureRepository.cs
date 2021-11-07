using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductPicture;
using SM.Domain.ProductPictureAgg;

namespace SM.Infrastructure.Repositories
{
    public class ProductPictureRepository : RepositoryBase<int, ProductPicture>, IProductPictureRepository
    {
        private readonly SMContext _smContext;


        public ProductPictureRepository(SMContext smContext) : base(smContext)
        {
            _smContext = smContext;
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _smContext.ProductPicture.Include(x => x.Product).Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Product = x.Product.Name,
                IsRemoved = x.IsRemoved,
                CreationTime = x.CreationTime.ToString(),
                Picture = x.Picture,
                ProductId = x.ProductId
            });
            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditProductPicture GetDetail(int id)
        {
            return _smContext.ProductPicture.Select(x => new EditProductPicture
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle

            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
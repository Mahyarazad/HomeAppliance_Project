using System.Collections.Generic;
using _0_Framework;
using SM.Application.Contracts.ProductPicture;

namespace SM.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<int, ProductPicture>
    {
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
        EditProductPicture GetDetail(int id);
    }
}
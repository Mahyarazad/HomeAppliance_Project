using System.Collections.Generic;
using _0_Framework.Application;

namespace SM.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);
        OperationResult Delete(int id);
        OperationResult Reactivate(int id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel command);
        EditProductPicture GetDetails(int Id);

    }
}
using System.Collections.Generic;
using _0_Framework.Application;

namespace SM.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);

        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetails(int Id);
        List<ProductViewModel> GetList();

    }
}
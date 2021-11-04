using System.Collections.Generic;
using _0_Framework.Application;

namespace SM.Application.Contracts
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        List<ProductCategoryViewModel> GetList();
        EditProductCategory GetDetails(int Id);

    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using _0_Framework;
using SM.Application.Contracts;

namespace ShopManagement.Domain
{
    public interface IProductCategoryRepository : IRepositoty<int, ProductCategory>
    {
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        EditProductCategory GetDetail(int id);
        List<ProductCategoryViewModel> GetCustomList();

    }
}
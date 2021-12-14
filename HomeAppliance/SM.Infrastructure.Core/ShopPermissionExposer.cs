using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace SM.Infrastructure.Core
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Exposer()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProduct, "List Product"),
                        new PermissionDto(ShopPermissions.EditProduct, "Edit Product"),
                        new PermissionDto(ShopPermissions.CreateProduct, "Create Product"),
                        new PermissionDto(ShopPermissions.SearchProduct, "Search Product"),
                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductCategory, "List Product Category"),
                        new PermissionDto(ShopPermissions.EditProductCategory, "Edit Product Category"),
                        new PermissionDto(ShopPermissions.CreateProductCategory, "Create Product Category"),
                        new PermissionDto(ShopPermissions.SearchProductCategory, "Search Product Category"),
                        new PermissionDto(ShopPermissions.Reactivate, "Reactivate"),
                        new PermissionDto(ShopPermissions.Deactivate, "Deactive"),
                    }
                }
            };
        }
    }
}
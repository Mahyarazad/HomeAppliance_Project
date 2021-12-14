using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace IM.Infrustructure.Core
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Exposer()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Invenory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.Create, "Create"),
                        new PermissionDto(InventoryPermissions.Edit, "Edit"),
                        new PermissionDto(InventoryPermissions.Incremenet, "Incremenet"),
                        new PermissionDto(InventoryPermissions.Decrement, "Decrement"),
                        new PermissionDto(InventoryPermissions.Search, "Search"),
                        new PermissionDto(InventoryPermissions.GetList, "Get List"),
                        new PermissionDto(InventoryPermissions.GetOperationsLogs, "Get Operation Logs"),
                    }
                }
            };
        }
    }
}
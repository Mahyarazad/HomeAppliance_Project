using System.Collections.Generic;
using _0_Framework;
using Inventory.Management.Application.Contracts;

namespace InventoryManagement.Domain
{
    public interface IInventoryRepository : IRepositoty<int, Inventory>
    {
        EditInventory GetDetails(int Id);
        Inventory GetBy(long ProductId);
        List<InventoryViewModel> Search(InventorySearchModel search);
    }
}
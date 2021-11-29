using System.Collections.Generic;
using _0_Framework;
using IM.Application.Contracts;

namespace IM.Domain
{
    public interface IInventoryRepository : IRepositoty<int, Inventory>
    {
        EditInventory GetDetails(int Id);
        Inventory GetBy(int ProductId);
        List<InventoryOperation> GetLog(int Id);
        List<InventoryViewModel> Search(InventorySearchModel search);
    }
}
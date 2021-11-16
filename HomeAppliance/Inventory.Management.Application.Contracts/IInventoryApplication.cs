using System.Collections.Generic;
using _0_Framework.Application;

namespace IM.Application.Contracts
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increment(IncrementInventory command);
        OperationResult Decrement(List<DecrementInventory> command);
        OperationResult Decrement(DecrementInventory command);
        EditInventory GetDetail(int Id);
        List<InventoryViewModel> Search(InventorySearchModel search);
        List<InventoryOperationLog> GetOperationLogs(int Id);
    }
}
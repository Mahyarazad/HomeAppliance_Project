using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework;
using _0_Framework.Application;
using IM.Application.Contracts;
using IM.Domain;

namespace IM.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operationResult = new OperationResult();
            if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId))
                return operationResult.Failed(ApplicationMessage.RecordExists);

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operationResult = new OperationResult();
            var target = _inventoryRepository.GetBy(command.ProductId);
            if (target == null)
                return operationResult.Failed(ApplicationMessage.RecordNotFound);
            if (_inventoryRepository.Exist(x => x.ProductId == command.Id && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessage.RecordExists);

            target.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Increment(IncrementInventory command)
        {
            const long operatorId = 1;
            const long orderId = 1;
            var operationResult = new OperationResult();
            var target = _inventoryRepository.Get(command.InventoryId);

            if (target == null)
                return operationResult.Failed(ApplicationMessage.RecordNotFound);
            target.Increment(true, command.Description, command.Count, orderId, operatorId);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public OperationResult Decrement(List<DecrementInventory> command)
        {
            const long operatorId = 1;
            const long orderId = 1;
            var operationResult = new OperationResult();
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Decrement(false, item.Description, item.Count, orderId, operatorId);
            }
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Decrement(DecrementInventory command)
        {
            var operationResult = new OperationResult();
            var target = _inventoryRepository.Get(command.InventoryId);

            if (target == null)
                return operationResult.Failed(ApplicationMessage.RecordNotFound);

            if (target.Count - command.Count < 0)
                return operationResult.Failed(
                    $"Current Inventory is {target.Count}, and you cannot reduce more than available stock in the Inventory.");

            target.Decrement(true, command.Description, command.Count, command.orderId, command.operatorId);
            _inventoryRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditInventory GetDetail(int Id)
        {
            return _inventoryRepository.GetDetails(Id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel search)
        {
            return _inventoryRepository.Search(search);
        }

        public List<InventoryViewModel> GetList()
        {
            return _inventoryRepository.GetList().Select(x => new InventoryViewModel
            {
                CurrentCount = x.Count,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                IsInStock = x.IsInStock,
            }).ToList();
        }

        public List<InventoryOperationLog> GetOperationLogs(int Id)
        {
            var inventory = _inventoryRepository.GetLog(Id);
            return inventory.Select(x => new InventoryOperationLog
            {
                Id = x.Id,
                Count = x.Count,
                Description = x.Description,
                CurrentStock = x.CurrentStock,
                InventoryId = x.InventoryId,
                OperationTime = x.OperationTime,
                OperationType = x.OperationType,
                OperatorId = x.OperatorId,
                OrderId = x.OrderId
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}

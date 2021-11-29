using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using _0_Framework;

namespace Inventory.Domain
{
    public class Inventory : BaseEntity
    {
        public Inventory(int productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }

        public int ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }

        private long CalculateCurrentStock()
        {
            var incoming = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var outgoing = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return incoming - outgoing;
        }

        public void Increment(long count, long operatorId, string description)
        {
            var currentStock = count + CalculateCurrentStock();
            var operation = new InventoryOperation(true, count, currentStock, operatorId, 0, description, Id);
            Operations.Add(operation);

            IsInStock = currentStock > 0;
        }

        public void Decrement(long count, long operatorId, string description)
        {
            var currentStock = CalculateCurrentStock() - count;
            var operation = new InventoryOperation(false, count, currentStock, operatorId, 0, description, Id);
            Operations.Add(operation);

            IsInStock = currentStock > 0;
        }
    }


    public class InventoryOperation
    {
        public InventoryOperation(bool operation, long count, long currentStock, long operatorId
            , int orderId, string description, int invertoryId)
        {
            Operation = operation;
            Count = count;
            CurrentStock = currentStock;
            OperatorId = operatorId;
            OrderId = orderId;
            OperationDate = DateTime.Now;
            Description = description;
            InvertoryId = invertoryId;
        }

        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public long Count { get; private set; }
        public long CurrentStock { get; private set; }
        public long OperatorId { get; private set; }
        public int OrderId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public string Description { get; private set; }
        public int InvertoryId { get; private set; }
        public Inventory Inventory { get; private set; }

    }
}

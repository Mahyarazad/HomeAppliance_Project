using System;

namespace Inventory.Management.Application.Contracts
{
    public class CreateInventory
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
    }
}

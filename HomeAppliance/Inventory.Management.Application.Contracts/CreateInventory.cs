using System;
using System.Collections.Generic;
using SM.Application.Contracts.Product;

namespace IM.Application.Contracts
{
    public class CreateInventory
    {
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public List<ProductViewModel> ProductList { get; set; }
    }
}

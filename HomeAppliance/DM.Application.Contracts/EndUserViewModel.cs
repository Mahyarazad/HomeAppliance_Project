using System;
using System.Collections.Generic;
using SM.Application.Contracts.Product;

namespace DM.Application.Contracts
{
    public class EndUserViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double DiscountRate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
        public string Occasion { get; set; }
        public string CreationTime { get; set; }
        public string ProductName { get; set; }
    }
}
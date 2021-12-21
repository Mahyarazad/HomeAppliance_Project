using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework;
using SM.Application.Contracts.Product;

namespace DM.Application.Contracts.EndUser
{
    public class DefineEndUserDiscount
    {
        [Range(1, 10000000000, ErrorMessage = "Please select a product")]
        public int ProductId { get; set; }
        [Range(0.5, 100, ErrorMessage = "The number should between 0.5 to 100")]
        public double DiscountRate { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public DateTime EndTime { get; set; }
        public string StartTimeString { get; set; }
        public string EndTimeString { get; set; }
        public string Occasion { get; set; }
        public string CreationDate { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}

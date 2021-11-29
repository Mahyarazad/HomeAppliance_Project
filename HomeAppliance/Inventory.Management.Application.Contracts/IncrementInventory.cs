using System.ComponentModel.DataAnnotations;
using _0_Framework;

namespace IM.Application.Contracts
{
    public class IncrementInventory
    {
        public int InventoryId { get; set; }
        [Range(1, 1000000000, ErrorMessage = "You should input a positive number!")]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long Count { get; set; }
        public string Description { get; set; }
        public long orderId { get; set; }
        public long operatorId { get; set; }
    }
}
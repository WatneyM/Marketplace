using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Maintenance.Order
{
    public class SupplementaryOA
    {
        public string? AttachedProduct { get; set; }
        [Required(ErrorMessage = "Delivery address is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Delivery amount is required")]
        [Range(1, 5, ErrorMessage = "Order can contain only up to 5 items")]
        public int Amount { get; set; }
    }
}
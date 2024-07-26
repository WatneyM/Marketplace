using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Product
{
    public class ProductRWAdapter
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Required. Field must contain a product name")]
        public string Product { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        [Required(ErrorMessage = "Required. Upload related image")]
        public string Image { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required. Field must contain a product price")]
        [Range(0, double.MaxValue, ErrorMessage = "Value out of range")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Required. Must be attached to category")]
        public string AttachedToCategory { get; set; } = string.Empty;

        public List<ProductAttributeRWAdapter> AttachedValues { get; set; } = [];
    }
}
using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Maintenance.Product
{
    public class ProductAttributeRWAdapter
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Required. Attribute must contain a value")]
        public string Value { get; set; } = string.Empty;

        public string AttachedToAttribute { get; set; } = string.Empty;
    }
}
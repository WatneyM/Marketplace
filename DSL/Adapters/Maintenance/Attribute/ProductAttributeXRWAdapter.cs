using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Maintenance.Attribute
{
    public class ProductAttributeXRWAdapter
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Required. Field must contain an attribute name")]
        public string Attribute { get; set; } = string.Empty;
    }
}
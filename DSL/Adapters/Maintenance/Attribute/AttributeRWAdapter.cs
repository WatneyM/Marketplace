using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Maintenance.Attribute
{
    public class AttributeRWAdapter
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Required. Field must contain an attribute name")]
        public string Attribute { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required. Must be attached at least to any group")]
        public string AttachedToGroup { get; set; } = string.Empty;
    }
}
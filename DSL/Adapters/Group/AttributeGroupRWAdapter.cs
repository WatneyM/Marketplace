using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Group
{
    public class AttributeGroupRWAdapter
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Required. Field must contain a group name")]
        public string Group { get; set; } = string.Empty;

        [Required(ErrorMessage = "Required. Must be attached to category")]
        public string AttachedToCategory { get; set; } = string.Empty;
    }
}
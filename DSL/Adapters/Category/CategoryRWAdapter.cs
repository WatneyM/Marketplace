using System.ComponentModel.DataAnnotations;

namespace DSL.Adapters.Category
{
    public class CategoryRWAdapter
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Required. Field must contain a category name")]
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? AttachedToCategory { get; set; }
    }
}
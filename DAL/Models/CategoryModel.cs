using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("CategoryStorage")]
    public class CategoryModel
    {
        [Key]
        [Column(Order = 1)]
        public string Key { get; set; } = string.Empty;

        [Column("Category", Order = 2)]
        public string Category { get; set; } = string.Empty;

        [Column("Attached Category Key", Order = 3)]
        public string? AttachedToCategory { get; set; }
        [ForeignKey("AttachedToCategory")]
        public CategoryModel? CategoryNav { get; set; }

        [Column("Attachable", Order = 4)]
        public bool Attachable { get; set; } = true;

        [Column("Created At", Order = 5)]
        public DateTime CreatedAt { get; set; }
        [Column("Modified At", Order = 6)]
        public DateTime ModifiedAt { get; set; }

        public List<ProductModel>? AttachedProducts { get; set; }
        public List<AttributeGroupModel>? AttachedGroups { get; set; }
    }
}
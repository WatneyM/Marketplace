using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("AttributeGroupStorage")]
    public class AttributeGroupModel
    {
        [Key]
        [Column(Order = 1)]
        public string Key { get; set; } = string.Empty;

        [Column("Group", Order = 2)]
        public string Group { get; set; } = string.Empty;

        [Column("Attached Category Key", Order = 3)]
        public string? AttachedToCategory { get; set; }
        [ForeignKey("AttachedToCategory")]
        public CategoryModel? CategoryNav { get; set; }

        [Column("Created At", Order = 4)]
        public DateTime CreatedAt { get; set; }
        [Column("Modified At", Order = 5)]
        public DateTime ModifiedAt { get; set; }

        public List<AttributeModel>? AttachedAttributes { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("AttributeStorage")]
    public class AttributeModel
    {
        [Key]
        [Column(Order = 1)]
        public string Key { get; set; } = string.Empty;

        [Column("Attribute", Order = 2)]
        public string Attribute { get; set; } = string.Empty;

        [Column("Attached To Group")]
        public string AttachedToGroup { get; set; } = string.Empty;
        [ForeignKey("AttachedToGroup")]
        public AttributeGroupModel? GroupNav { get; set; }

        [Column("Created At", Order = 4)]
        public DateTime CreatedAt { get; set; }
        [Column("Modified At", Order = 5)]
        public DateTime ModifiedAt { get; set; }

        public List<ProductAttributeModel>? AttachedValues { get; set; }
    }
}
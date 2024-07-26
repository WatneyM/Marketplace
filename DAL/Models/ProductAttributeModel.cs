using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("ProductAttributeStorage")]
    public class ProductAttributeModel
    {
        [Key]
        [Column(Order = 1)]
        public string Key { get; set; } = string.Empty;

        [Column("Value", Order = 2)]
        public string Value { get; set; } = string.Empty;

        [Column("Attached Product Key", Order = 3)]
        public string AttachedToProduct { get; set; } = string.Empty;
        [ForeignKey("AttachedToProduct")]
        public ProductModel? ProductNav { get; set; }

        [Column("Attached Attribute Key", Order = 4)]
        public string AttachedToAttribute { get; set; } = string.Empty;
        [ForeignKey("AttachedToAttribute")]
        public AttributeModel? AttributeNav { get; set; }
    }
}
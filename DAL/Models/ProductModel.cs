using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [Table("ProductStorage")]
    public class ProductModel
    {
        [Key]
        [Column(Order = 1)]
        public string Key { get; set; } = string.Empty;

        [Column("Product Code", Order = 2)]
        public string? Code { get; set; }
        [Column("Product", Order = 3)]
        public string Product { get; set; } = string.Empty;

        [Column("Short Description", Order = 4)]
        public string? ShortDescription { get; set; }
        [Column("Long Description", Order = 5)]
        public string? LongDescription { get; set; }
        [Column("Image", Order = 6)]
        public string Image { get; set; } = string.Empty;
        [Column("Price", Order = 7)]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Column("Attached Category Key", Order = 8)]
        public string AttachedToCategory { get; set; } = string.Empty;
        [ForeignKey("AttachedToCategory")]
        public CategoryModel? CategoryNav { get; set; }

        [Column("Created At", Order = 9)]
        public DateTime CreatedAt { get; set; }
        [Column("Modified At", Order = 10)]
        public DateTime ModifiedAt { get; set; }

        public List<ProductAttributeModel> AttachedValues { get; set; } = [];
    }
}
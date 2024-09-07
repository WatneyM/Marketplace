using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("SupplyStorage")]
    public class SupplyModel
    {
        [Key]
        [Column(Order = 1)]
        public string? Key { get; set; }
        [Column("Available Amount", Order = 2)]
        public int Amount { get; set; }
        [Column("Attached To Category", Order = 3)]
        public string? OfCategory { get; set; }

        [Column("Attached Product", Order = 4)]
        public string AttachedProduct { get; set; } = string.Empty;
        [ForeignKey("AttachedProduct")]
        public ProductModel? ProductNav { get; set; }
    }
}
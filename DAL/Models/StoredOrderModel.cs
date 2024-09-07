using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using DAL.Enums;

namespace DAL.Models
{
    [Table("StoredOrderStorage")]
    public class StoredOrderModel
    {
        [Key]
        [Column(Order = 1)]
        public string? Key { get; set; }

        [Column("Order No", Order = 2)]
        public string? OrderNo { get; set; }
        [Column("Address", Order = 3)]
        public string? Address { get; set; }
        [Column("Ordered Amount")]
        public int Amount { get; set; }

        [Column("Order Status", Order = 4)]
        public OrderCompletionState State { get; set; }

        [Column("Attached Product", Order = 5)]
        public string AttachedProduct { get; set; } = string.Empty;
        [ForeignKey("AttachedProduct")]
        public ProductModel? ProductNav { get; set; }

        [Column("Attached User", Order = 6)]
        public string? AttachedUsername { get; set; }

        [Column("Ordered At", Order = 7)]
        public DateTime? WhenOrdered { get; set; }
        [Column("Unordered At", Order = 8)]
        public DateTime? WhenUnordered { get; set; }
    }
}
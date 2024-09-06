using DAL.Enums;

namespace DSL.Adapters.Maintenance.Order
{
    public class StoredOrderAdapter
    {
        public string Key { get; set; } = string.Empty;
        public string OrderNo { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public int Amount { get; set; }
        public OrderCompletionState Status { get; set; }
        public string AttachedProduct { get; set; } = string.Empty;
        public string AttachedUsername { get; set; } = string.Empty;
        public DateTime WhenOrdered { get; set; }
        public DateTime WhenUnordered { get; set; }
    }
}
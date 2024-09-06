using DAL.Enums;
using DSL.Adapters.Maintenance.Order;

namespace DSL.Services.Declarations
{
    public interface IOrderService
    {
        public OrderRAdapter GetOrder(string key);
        public IEnumerable<OrderRAdapter> GetOrders(OrderProcessingState status);
        public IEnumerable<OrderRAdapter> GetOrders(string username);
        public IEnumerable<StoredOrderAdapter> GetStored(OrderCompletionState state);

        public bool MakeOrder(OrderWAdapter orderData);
        public bool RemoveOrder(string orderKey);

        public bool NextState(string orderKey, OrderProcessingState state);
        public bool StoreOrder(string orderKey, OrderCompletionState state);
    }
}
using DAL.Enums;
using DAL.Models;

namespace BLL.Managers.Declarations
{
    public interface IOrderManager
    {
        public bool Has(string dbKey);
        public OrderModel? Track(string dbKey);

        public OrderModel? Read(string dbKey);
        public IEnumerable<StoredOrderModel> ReadStored(OrderCompletionState state);
        public IEnumerable<OrderModel> ReadOrders(OrderProcessingState status);
        public IEnumerable<OrderModel> ReadOrders(string username);

        public bool Create(OrderModel order);
        public bool Remove(OrderModel order);

        public bool SetState(OrderModel model, OrderProcessingState state);
        public bool Store(OrderModel model, OrderCompletionState state);
    }
}
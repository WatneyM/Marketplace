using AutoMapper;

using BLL.Managers.Declarations;
using DAL.Enums;
using DAL.Models;

using DSL.Adapters.Maintenance.Order;
using DSL.Services.Declarations;

namespace DSL.Services.Implementations
{
    public class OrderService(IOrderManager manager) : IOrderService
    {
        private readonly Mapper _orderToAdapter = new(new MapperConfiguration(cfg
            => cfg.CreateMap<OrderModel, OrderRAdapter>()));
        private readonly Mapper _storedToAdapter = new(new MapperConfiguration(cfg
            => cfg.CreateMap<StoredOrderModel, StoredOrderAdapter>()));
        private readonly Mapper _adapterToOrder = new(new MapperConfiguration(cfg
            => cfg.CreateMap<OrderWAdapter, OrderModel>()));

        private readonly IOrderManager _manager = manager;

        public OrderRAdapter GetOrder(string key)
        {
            return _orderToAdapter.Map<OrderRAdapter>(_manager.Read(key));
        }

        public IEnumerable<OrderRAdapter> GetOrders(OrderProcessingState status)
        {
            return _orderToAdapter.Map<IEnumerable<OrderRAdapter>>(_manager.ReadOrders(status));
        }

        public IEnumerable<OrderRAdapter> GetOrders(string username)
        {
            return _orderToAdapter.Map<IEnumerable<OrderRAdapter>>(_manager.ReadOrders(username));
        }

        public IEnumerable<StoredOrderAdapter> GetStored(OrderCompletionState state)
        {
            return _storedToAdapter.Map<IEnumerable<StoredOrderAdapter>>(_manager.ReadStored(state));
        }

        public bool MakeOrder(OrderWAdapter orderData)
        {
            return _manager.Create(_adapterToOrder.Map<OrderModel>(orderData));
        }

        public bool RemoveOrder(string orderKey)
        {
            return _manager.Has(orderKey) && _manager.Remove(_manager.Read(orderKey)!);
        }

        public bool NextState(string orderKey, OrderProcessingState state)
        {
            return _manager.Has(orderKey) && _manager.SetState(_manager.Track(orderKey)!, state);
        }

        public bool StoreOrder(string orderKey, OrderCompletionState state)
        {
            return _manager.Has(orderKey) && _manager.Store(_manager.Track(orderKey)!, state);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System.Text;

using BLL.Managers.Declarations;

using DAL;
using DAL.Enums;
using DAL.Models;

namespace BLL.Managers.Implementations
{
    public class OrderManager(ApplicationContext context) : IOrderManager
    {
        private readonly ApplicationContext _context = context;
        private readonly DbSet<OrderModel> _set = context.Set<OrderModel>();

        public bool Has(string dbKey)
        {
            return _set.AsNoTracking().Any(p => p.Key == dbKey);
        }

        public OrderModel? Track(string dbKey)
        {
            return _set.Single(p => p.Key == dbKey);
        }

        public OrderModel? Read(string dbKey)
        {
            return _set.AsNoTracking()
                .Single(p => p.Key == dbKey);
        }

        public IEnumerable<StoredOrderModel> ReadStored(OrderCompletionState state)
        {
            return _context.StoredOrders.AsNoTracking()
                .Where(p => p.State == state)
                .AsEnumerable();
        }

        public IEnumerable<OrderModel> ReadOrders(OrderProcessingState state)
        {
            return _set.AsNoTracking()
                .Where(p => p.State == state)
                .AsEnumerable();
        }

        public IEnumerable<OrderModel> ReadOrders(string username)
        {
            return _set.AsNoTracking()
                .Where(p => p.AttachedUsername == username)
                .AsEnumerable();
        }

        public bool Create(OrderModel order)
        {
            order.OrderNo = GenerateOrderNo();
            order.WhenOrdered = DateTime.Now;

            try
            {
                _set.Add(order);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Remove(OrderModel order)
        {
            try
            {
                _set.Remove(order);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool SetState(OrderModel model, OrderProcessingState state)
        {
            model.State = state;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Store(OrderModel model, OrderCompletionState state)
        {
            StoredOrderModel storedOrder = new StoredOrderModel()
            {
                Key = model.Key,
                OrderNo = model.OrderNo,
                Address = model.Address,
                Amount = model.Amount,
                AttachedProduct = model.AttachedProduct,
                AttachedUsername = model.AttachedUsername,
                State = state,
                WhenOrdered = model.WhenOrdered,
                WhenUnordered = DateTime.Now
            };

            try
            {
                _context.StoredOrders.Add(storedOrder);
                _context.SaveChanges();

                _set.Local.Remove(model);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private int OrderNoFactory(DateTime date)
        {
            return _set.AsNoTracking()
                .Where(p => p.WhenOrdered!.Value.Day == date.Day).Count()
                 + _context.StoredOrders.AsNoTracking()
                 .Where(p => p.WhenOrdered!.Value.Day == date.Day).Count();
        }

        private string GenerateOrderNo()
        {
            StringBuilder builder = new();

            return builder.AppendFormat("{0}{1}{2}-{3}", DateTime.Now.Month,
                DateTime.Now.Day, DateTime.Now.Year, OrderNoFactory(DateTime.Now)).ToString();
        }
    }
}
using Microsoft.EntityFrameworkCore;

using BLL.Managers.Declarations;

using DAL;
using DAL.Models;

namespace BLL.Managers.Implementations
{
    public class SupplyManager(ApplicationContext context) : ISupplyManager
    {
        private readonly ApplicationContext _context = context;
        private readonly DbSet<SupplyModel> _set = context.Set<SupplyModel>();

        public bool CheckKey(string key)
        {
            return _set.AsNoTracking().Any(p => p.Key == key);
        }

        public bool CheckProp(string key)
        {
            return _set.AsNoTracking().Any(p => p.AttachedProduct == key);
        }

        public SupplyModel? Track(string dbKey)
        {
            return _set.Single(p => p.AttachedProduct == dbKey);
        }

        public SupplyModel Read(string dbKey)
        {
            return _set.AsNoTracking()
                .Single(p => p.AttachedProduct == dbKey);
        }

        public IEnumerable<SupplyModel> GetProductsOfCategory(string categoryKey)
        {
            return _set.AsNoTracking()
                .Where(p => p.OfCategory == categoryKey).AsEnumerable();
        }

        public bool SetAmount(IEnumerable<SupplyModel> models)
        {
            List<SupplyModel> originals = [];

            for (int idx = 0; idx < models.Count(); idx++)
            {
                originals.Add(_set.Find(models.ElementAt(idx).Key)!);
                originals[idx].Amount = models.ElementAt(idx).Amount;
            }

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

        public bool CreateAmount(SupplyModel model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Increase(SupplyModel model, int amount)
        {
            model.Amount += amount;

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

        public bool Decrease(SupplyModel model, int amount)
        {
            model.Amount -= amount;

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
    }
}
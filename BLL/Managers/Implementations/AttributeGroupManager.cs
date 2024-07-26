using Microsoft.EntityFrameworkCore;

using DAL;
using DAL.Models;
using BLL.Managers.Declarations;

namespace BLL.Managers.Implementations
{
    public class AGModelMaintenanceManager(ApplicationContext context)
        : IModelManager<AttributeGroupModel>, IAttributeGroupManager
    {
        private readonly ApplicationContext _context = context;
        private readonly DbSet<AttributeGroupModel> _set = context
            .Set<AttributeGroupModel>();

        public bool Has(string dbKey)
        {
            return _set.AsNoTracking().Any(p => p.Key == dbKey);
        }

        public AttributeGroupModel? Track(string dbKey)
        {
            return _set.Find(dbKey);
        }

        public AttributeGroupModel? Read(string dbKey)
        {
            return _set.AsNoTracking()
                .SingleOrDefault(p => p.Key == dbKey);
        }

        public IEnumerable<AttributeGroupModel> ReadAll()
        {
            return _set.AsNoTracking()
                .Include(p => p.CategoryNav!)
                .AsEnumerable();
        }

        public bool Create(AttributeGroupModel model)
        {
            model.CreatedAt = DateTime.Now;
            model.ModifiedAt = model.CreatedAt;

            try
            {
                _set.Add(model);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(AttributeGroupModel model)
        {
            var original = Track(model.Key)!;

            original.Group = model.Group;
            original.AttachedToCategory = model.AttachedToCategory;
            original.ModifiedAt = DateTime.Now;

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

        public bool Delete(AttributeGroupModel model)
        {
            try
            {
                _set.Remove(model);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<AttributeGroupModel> ReadByCategoryKey(string categoryKey)
        {
            return _set.AsNoTracking()
                .Where(p => p.AttachedToCategory == categoryKey)
                .Include(s => s.AttachedAttributes)
                .AsEnumerable();
        }
    }
}
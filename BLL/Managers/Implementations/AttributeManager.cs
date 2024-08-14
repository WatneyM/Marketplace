using Microsoft.EntityFrameworkCore;

using BLL.Managers.Declarations;

using DAL;
using DAL.Models;

namespace BLL.Managers.Implementations
{
    public class AttributeManager(ApplicationContext context)
        : IModelManager<AttributeModel>, IAttributeManager
    {
        private readonly ApplicationContext _context = context;
        private readonly DbSet<AttributeModel> _set = context
            .Set<AttributeModel>();

        public int Count()
        {
            return _set.Count();
        }

        public bool Has(string dbKey)
        {
            return _set.AsNoTracking().Any(p => p.Key == dbKey);
        }

        public AttributeModel? Track(string dbKey)
        {
            return _set.Single(p => p.Key == dbKey);
        }

        public AttributeModel? Read(string dbKey)
        {
            return _set.AsNoTracking()
                .SingleOrDefault(p => p.Key == dbKey);
        }

        public IEnumerable<AttributeModel> ReadAll()
        {
            return _set.AsNoTracking()
                .Include(p => p.GroupNav)
                .AsEnumerable();
        }

        public bool Create(AttributeModel model)
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

        public bool Update(AttributeModel model)
        {
            AttributeModel original = Track(model.Key)!;

            original.Attribute = model.Attribute;
            original.AttachedToGroup = model.AttachedToGroup;
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

        public bool Delete(AttributeModel model)
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
    }
}
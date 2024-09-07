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

        public IEnumerable<KeyValuePair<string, string>> ReadFiltersNames(string categoryKey)
        {
            return _set.AsNoTracking()
                .Include(p => p.GroupNav)
                .Where(p => p.GroupNav!.AttachedToCategory == categoryKey)
                .Where(p => p.UseAsFilter)
                .Select(s => new KeyValuePair<string, string>(s.Key, s.Attribute))
                .AsEnumerable();
        }

        public IEnumerable<string> ReadFilters(IEnumerable<string> keys)
        {
            return _set.AsNoTracking()
                .Where(p => keys.Contains(p.Key) && p.UseAsFilter)
                .Select(s => s.Key)
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
            original.UseAsFilter = model.UseAsFilter;
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

        public IEnumerable<KeyValuePair<string, string>> ReadAttributeValues(IEnumerable<string> keys)
        {
            List<KeyValuePair<string, string>> result = [];
            List<ProductAttributeModel> source = [.. _context.AttributesData
                .AsNoTracking()
                .Where(p => keys.Contains(p.AttachedToAttribute))];

            foreach (ProductAttributeModel item in source)
            {
                if (result.Contains(new KeyValuePair<string, string>(item.AttachedToAttribute, item.Value))) continue;
                else result.Add(new KeyValuePair<string, string>(item.AttachedToAttribute, item.Value));
            }

            return result.AsEnumerable();
        }
    }
}
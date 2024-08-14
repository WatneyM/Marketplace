using Microsoft.EntityFrameworkCore;

using BLL.Managers.Declarations;

using DAL;
using DAL.Models;

namespace BLL.Managers.Implementations
{
    public class CategoryManager(ApplicationContext context)
        : IModelManager<CategoryModel>, ICategoryManager
    {
        private readonly ApplicationContext _context = context;
        private readonly DbSet<CategoryModel> _set = context
            .Set<CategoryModel>();

        public int Count()
        {
            return _set.Count();
        }

        public bool Has(string dbKey)
        {
            return _set.AsNoTracking().Any(p => p.Key == dbKey);
        }

        public CategoryModel? Track(string dbKey)
        {
            return _set.Find(dbKey);
        }

        public CategoryModel? Read(string dbKey)
        {
            return _set.AsNoTracking()
                .SingleOrDefault(p => p.Key == dbKey);
        }

        public IEnumerable<CategoryModel> ReadAll()
        {
            return _set.AsNoTracking()
                .AsEnumerable();
        }

        public bool Create(CategoryModel model)
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

        public bool Update(CategoryModel model)
        {
            var original = Track(model.Key)!;

            original.Category = model.Category;
            original.Description = model.Description;
            original.ModifiedAt = DateTime.Now;
            original.AttachedToCategory = model.AttachedToCategory;

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

        public bool Delete(CategoryModel model)
        {
            if (model.AttachedGroups is not null
                && model.AttachedGroups.Count != 0) return false;

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

        public IEnumerable<CategoryModel> ReadAttachables()
        {
            return _set.AsNoTracking()
                .Where(p => p.AttachedToCategory != null)
                .AsEnumerable();
        }

        public IEnumerable<CategoryModel> ReadPrimaries()
        {
            return _set.AsNoTracking()
                .Where(p => p.AttachedToCategory == null)
                .AsEnumerable();
        }
    }
}
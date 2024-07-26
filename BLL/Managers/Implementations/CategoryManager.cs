using Microsoft.EntityFrameworkCore;

using DAL;
using DAL.Models;
using BLL.Managers.Declarations;

namespace BLL.Managers.Implementations
{
    public class CategoryManager(ApplicationContext context)
        : IModelManager<CategoryModel>, ICategoryManager
    {
        private readonly ApplicationContext _context = context;
        private readonly DbSet<CategoryModel> _set = context
            .Set<CategoryModel>();

        private void AddAssociatedStatus(string parentKey)
        {
            CategoryModel parent = Track(parentKey)!;

            if (!HasAttached(parent.Key, 1))
                parent.Attachable = true;
        }

        private void RemoveAssociatedStatus(string parentKey)
        {
            CategoryModel parent = Track(parentKey)!;

            parent.Attachable = false;
        }

        private void DetachPrevParent(string prevParentKey)
        {
            CategoryModel parent = Track(prevParentKey)!;

            if (!HasAttached(prevParentKey, 1))
                parent.Attachable = true;
        }

        private void AttachNextParent(string nextParentKey)
        {
            CategoryModel parent = Track(nextParentKey)!;

            if (!HasAttached(nextParentKey))
                parent.Attachable = false;
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

            if (model.AttachedToCategory is not null)
                RemoveAssociatedStatus(model.AttachedToCategory);

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
            original.ModifiedAt = DateTime.Now;

            if (model.AttachedToCategory is not null
                && original.AttachedToCategory is not null
                && model.AttachedToCategory != original.AttachedToCategory)
            {
                DetachPrevParent(original.AttachedToCategory);
                AttachNextParent(model.AttachedToCategory);
            }
            else if (model.AttachedToCategory is null
                && original.AttachedToCategory is not null)
                DetachPrevParent(original.AttachedToCategory);
            else if (model.AttachedToCategory is not null
                && original.AttachedToCategory is null)
                AttachNextParent(model.AttachedToCategory);

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
            if (model.AttachedToCategory is not null)
                AddAssociatedStatus(model.AttachedToCategory);

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

        public bool HasAttached(string categoryKey)
        {
            return _set.AsNoTracking()
                .Where(p => p.AttachedToCategory == categoryKey)
                .Count() == 1;
        }

        public bool HasAttached(string categoryKey, int requiredAmount)
        {
            return _set.AsNoTracking()
                .Where(p => p.AttachedToCategory == categoryKey)
                .Count() > requiredAmount;
        }

        public IEnumerable<CategoryModel> ReadAttachables()
        {
            return _set.AsNoTracking()
                .Where(p => p.Attachable)
                .AsEnumerable();
        }
    }
}
using Microsoft.EntityFrameworkCore;

using BLL.Managers.Declarations;

using DAL;
using DAL.Models;

namespace BLL.Managers.Implementations
{
    public class ProductManager(ApplicationContext context)
        : IModelManager<ProductModel>, IProductManager
    {
        private readonly ApplicationContext _context = context;
        private readonly DbSet<ProductModel> _set = context
            .Set<ProductModel>();

        public int Count()
        {
            return _set.Count();
        }

        public bool Has(string dbKey)
        {
            return _set.AsNoTracking().Any(p => p.Key == dbKey);
        }

        public ProductModel? Track(string dbKey)
        {
            return _set.Include(p => p.AttachedValues)
                .Single(p => p.Key == dbKey);
        }

        public ProductModel? Read(string dbKey)
        {
            return _set.AsNoTracking()
                .Include(s => s.AttachedValues)
                .Single(p => p.Key == dbKey);
        }

        public IEnumerable<ProductModel> ReadAll()
        {
            return _set.AsNoTracking().AsEnumerable();
        }

        public bool Create(ProductModel model)
        {
            model.CreatedAt = DateTime.Now;
            model.ModifiedAt = DateTime.Now;

            foreach (ProductAttributeModel attribute in model.AttachedValues!)
                attribute.AttachedToProduct = model.Key;

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

        public bool Update(ProductModel model)
        {
            ProductModel original = Track(model.Key)!;

            original.Product = model.Product;
            original.ShortDescription = model.ShortDescription;
            original.LongDescription = model.LongDescription;
            original.Price = model.Price;
            original.Image = model.Image;
            original.ModifiedAt = DateTime.Now;

            foreach (ProductAttributeModel attribute in model.AttachedValues)
            {
                if (original.AttachedValues
                    .Any(p => p.AttachedToAttribute == attribute.AttachedToAttribute))
                {
                    original.AttachedValues
                        .Find(p => p.AttachedToAttribute == attribute.AttachedToAttribute)!
                        .Value = attribute.Value;
                }
                else
                {
                    attribute.AttachedToProduct = model.Key;
                    original.AttachedValues.Add(attribute);
                }
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

        public bool Delete(ProductModel model)
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

        public int Count(string dbKey)
        {
            return _set.AsNoTracking()
                .Where(p => p.AttachedToCategory == dbKey)
                .Count();
        }

        public ProductModel ReadProductOfCategory(string categoryKey)
        {
            return _set.AsNoTracking()
                .Single(p => p.AttachedToCategory == categoryKey);
        }

        public IEnumerable<ProductModel> ReadProductsOfCategory(string categoryKey)
        {
            return _set.AsNoTracking()
                .Where(p => p.AttachedToCategory == categoryKey)
                .AsEnumerable();
        }
    }
}
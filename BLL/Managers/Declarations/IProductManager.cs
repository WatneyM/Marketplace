using DAL.Models;

namespace BLL.Managers.Declarations
{
    public interface IProductManager : IModelManager<ProductModel>
    {
        public int Count(string dbKey);

        public ProductModel ReadProductOfCategory(string categoryKey);
        public IEnumerable<ProductModel> ReadProductsOfCategory(string categoryKey);
    }
}
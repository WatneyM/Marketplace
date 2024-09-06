using DAL.Models;

namespace BLL.Managers.Declarations
{
    public interface IProductManager : IModelManager<ProductModel>
    {
        public int Count(string dbKey);

        public IEnumerable<ProductModel> Read(IEnumerable<string> keys);
        public ProductModel ReadProductOfCategory(string categoryKey);
        public IEnumerable<ProductModel> ReadProductsOfCategory(string categoryKey);
        public IEnumerable<KeyValuePair<string, string>> ReadProductNames(IEnumerable<string> keys);
    }
}
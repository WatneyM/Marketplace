using DSL.Adapters.Maintenance.Product;

namespace DSL.Services.Declarations
{
    public interface IProductService
    {
        public bool KeyCheck(string key);

        public int GetCount();
        public int GetCountOfCategory(string categoryKey);

        public ProductRWAdapter GetProduct(string productKey);
        public ProductRAdapter GetProductInfo(string productKey);
        public ProductRAdapter GetProductOfCategory(string categoryKey);

        public IEnumerable<ProductRAdapter> GetProductsOfCategory(string categoryKey);
        public IEnumerable<ProductRAdapter> GetProductsAsList();

        public IEnumerable<ShortOrderAdapter> GetProducts(IEnumerable<string> keys);
        public IEnumerable<KeyValuePair<string, string>> GetProductNames(IEnumerable<string?> keys);

        public bool PushOrModifyProduct(ProductRWAdapter adapter);
        public bool DropProduct(string productKey);
    }
}
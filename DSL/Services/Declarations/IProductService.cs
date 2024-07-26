using DSL.Adapters.Product;

namespace DSL.Services.Declarations
{
    public interface IProductService
    {
        public ProductRWAdapter GetProduct(string productKey);

        public IEnumerable<ProductRAdapter> GetProductsAsList();

        public bool PushOrModifyProduct(ProductRWAdapter adapter);
        public bool DropProduct(string productKey);
    }
}
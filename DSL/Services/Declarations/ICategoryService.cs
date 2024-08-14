using DSL.Adapters.Category;

namespace DSL.Services.Declarations
{
    public interface ICategoryService
    {
        public CategoryRWAdapter GetCategory(string categoryKey);

        public IEnumerable<CategoryRWAdapter> GetCategories();
        public IEnumerable<CategoryRAdapter> GetAttachableCategories();
        public IEnumerable<CategoryRAdapter> GetPrimaryCategories();
        public IEnumerable<CategoryRAdapter> GetPrimaryCategories(string exceptKey);

        public IEnumerable<CategoryRAdapter> GetCategoriesAsList();

        public bool PushOrModifyCategory(CategoryRWAdapter adapter);
        public bool DropCategory(string categoryKey);
    }
}
using DSL.Adapters.Maintenance.Category;

namespace DSL.Services.Declarations
{
    public interface ICategoryService
    {
        public bool KeyCheck(string key);

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
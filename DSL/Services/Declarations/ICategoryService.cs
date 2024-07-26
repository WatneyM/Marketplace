using DSL.Adapters.Category;

namespace DSL.Services.Declarations
{
    public interface ICategoryService
    {
        public CategoryRWAdapter GetCategory(string categoryKey);

        public IEnumerable<CategoryRWAdapter> GetCategories();
        public IEnumerable<CategoryRWAdapter> GetAttachableCategories();
        public IEnumerable<CategoryRWAdapter> GetCategoriesExceptKey(string categoryKey);

        public IEnumerable<CategoryRAdapter> GetCategoriesAsList();

        public bool PushOrModifyCategory(CategoryRWAdapter adapter);
        public bool DropCategory(string categoryKey);
    }
}
using AutoMapper;

using BLL.Managers.Declarations;

using DAL.Models;

using DSL.Services.Declarations;
using DSL.Adapters.Category;

namespace DSL.Services.Implementations
{
    public class CategoryService(ICategoryManager manager)
        : ICategoryService
    {
        private readonly ICategoryManager _manager = manager;

        private readonly Mapper _rwAToModel = new(new MapperConfiguration(cfg
            => cfg.CreateMap<CategoryRWAdapter, CategoryModel>()));

        private readonly Mapper _modelToRWA = new(new MapperConfiguration(cfg
            => cfg.CreateMap<CategoryModel, CategoryRWAdapter>()));
        private readonly Mapper _modelToRA = new(new MapperConfiguration(cfg
            => cfg.CreateMap<CategoryModel, CategoryRAdapter>()));

        public CategoryRWAdapter GetCategory(string categoryKey)
        {
            return _modelToRWA
                .Map<CategoryRWAdapter>(_manager
                .Read(categoryKey));
        }

        public IEnumerable<CategoryRWAdapter> GetCategories()
        {
            return _modelToRWA
                .Map<IEnumerable<CategoryRWAdapter>>(_manager
                .ReadAll());
        }

        public IEnumerable<CategoryRWAdapter> GetAttachableCategories()
        {
            return _modelToRWA
                .Map<IEnumerable<CategoryRWAdapter>>(_manager
                .ReadAttachables());
        }

        public IEnumerable<CategoryRWAdapter> GetCategoriesExceptKey(string categoryKey)
        {
            return _modelToRWA
                .Map<IEnumerable<CategoryRWAdapter>>(_manager
                .ReadAll()
                .Where(p => p.Key != categoryKey));
        }

        public IEnumerable<CategoryRAdapter> GetCategoriesAsList()
        {
            return _modelToRA
                .Map<IEnumerable<CategoryRAdapter>>(_manager
                .ReadAll());
        }

        public bool PushOrModifyCategory(CategoryRWAdapter adapter)
        {
            return _manager.Has(adapter.Key)
                ? _manager.Update(_rwAToModel.Map<CategoryModel>(adapter))
                : _manager.Create(_rwAToModel.Map<CategoryModel>(adapter));
        }

        public bool DropCategory(string categoryKey)
        {
            return _manager.Has(categoryKey) &&
                _manager.Delete(_manager.Track(categoryKey)!);
        }
    }
}
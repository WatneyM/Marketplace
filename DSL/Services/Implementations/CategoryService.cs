﻿using AutoMapper;

using BLL.Managers.Declarations;

using DAL.Models;

using DSL.Services.Declarations;
using DSL.Adapters.Maintenance.Category;

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

        public bool KeyCheck(string key) => _manager.Has(key);

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

        public IEnumerable<CategoryRAdapter> GetAttachableCategories()
        {
            return _modelToRA
                .Map<IEnumerable<CategoryRAdapter>>(_manager
                .ReadAttachables());
        }

        public IEnumerable<CategoryRAdapter> GetPrimaryCategories()
        {
            return _modelToRA
                .Map<IEnumerable<CategoryRAdapter>>(_manager
                .ReadPrimaries());
        }

        public IEnumerable<CategoryRAdapter> GetPrimaryCategories(string exceptKey)
        {
            return _modelToRA
                .Map<IEnumerable<CategoryRAdapter>>(_manager
                .ReadPrimaries()
                .Where(p => p.Key != exceptKey));
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
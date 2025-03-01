﻿using AutoMapper;

using BLL.Managers.Declarations;

using DAL.Models;

using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace DSL.Services.Implementations
{
    public class ProductService(IProductManager manager)
        : IProductService
    {
        private readonly IProductManager _manager = manager;

        private readonly Mapper _rwAToModel = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ProductRWAdapter, ProductModel>();
            cfg.CreateMap<ProductAttributeRWAdapter, ProductAttributeModel>();
        }));

        private readonly Mapper _modelToRWA = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ProductModel, ProductRWAdapter>();
            cfg.CreateMap<ProductAttributeModel, ProductAttributeRWAdapter>();
        }));
        private readonly Mapper _modelToRA = new(new MapperConfiguration(cfg
            => cfg.CreateMap<ProductModel, ProductRAdapter>()));

        private readonly Mapper _mapper = new(new MapperConfiguration(cfg
            => cfg.CreateMap<ProductModel, ShortOrderAdapter>()));

        public bool KeyCheck(string key) => _manager.Has(key);

        public int GetCount()
        {
            return _manager.Count();
        }

        public int GetCountOfCategory(string categoryKey)
        {
            return _manager.Count(categoryKey);
        }

        public ProductRWAdapter GetProduct(string productKey)
        {
            return _modelToRWA
                .Map<ProductRWAdapter>(_manager.Read(productKey));
        }

        public ProductRAdapter GetProductInfo(string productKey)
        {
            return _modelToRA
                .Map<ProductRAdapter>(_manager.Read(productKey));
        }

        public ProductRAdapter GetProductOfCategory(string categoryKey)
        {
            return _modelToRA.Map<ProductRAdapter>(_manager
                .ReadProductOfCategory(categoryKey));
        }

        public IEnumerable<ProductRAdapter> GetProductsOfCategory(string categoryKey)
        {
            return _modelToRA.Map<IEnumerable<ProductRAdapter>>(_manager
                .ReadProductsOfCategory(categoryKey));
        }

        public IEnumerable<ProductRAdapter> GetProductsOfCategoryWithFilter(string categoryKey,
            IEnumerable<IEnumerable<string>> filter)
        {
            return _modelToRA.Map<IEnumerable<ProductRAdapter>>(_manager
                .ReadProductsOfCategoryWithFilter(categoryKey, filter));
        }

        public IEnumerable<ProductRAdapter> GetProductsAsList()
        {
            return _modelToRA
                .Map<IEnumerable<ProductRAdapter>>(_manager
                .ReadAll());
        }

        public IEnumerable<ShortOrderAdapter> GetProducts(IEnumerable<string> keys)
        {
            return _mapper.Map<IEnumerable<ShortOrderAdapter>>(_manager.Read(keys));
        }

        public IEnumerable<KeyValuePair<string, string>> GetProductNames(IEnumerable<string?> keys)
        {
            return _manager.ReadProductNames(keys!);
        }

        public bool PushOrModifyProduct(ProductRWAdapter adapter)
        {
            return _manager.Has(adapter.Key)
                ? _manager.Update(_rwAToModel.Map<ProductModel>(adapter))
                : _manager.Create(_rwAToModel.Map<ProductModel>(adapter));
        }

        public bool DropProduct(string productKey)
        {
            return _manager.Has(productKey) &&
                _manager.Delete(_manager.Read(productKey)!);
        }

        public IEnumerable<string> GetRelatedAttributeKeys(string categoryKey)
        {
            return _manager.ReadAttributeKeys(categoryKey);
        }
    }
}
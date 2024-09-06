using AutoMapper;

using BLL.Managers.Declarations;

using DAL.Models;

using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace DSL.Services.Implementations
{
    public class SupplyService(ISupplyManager manager) : ISupplyService
    {
        private readonly Mapper _modelToAdapter = new(new MapperConfiguration(cfg
            => cfg.CreateMap<SupplyModel, SupplyRAdapter>()));
        private readonly Mapper _pAToModel = new(new MapperConfiguration(cfg
            => cfg.CreateMap<SupplyRAdapter, SupplyModel>()));
        private readonly Mapper _fAToModel = new(new MapperConfiguration(cfg
            => cfg.CreateMap<SupplyWAdapter, SupplyModel>()));

        private readonly ISupplyManager _manager = manager;

        public SupplyRAdapter GetSupplyRecord(string key)
        {
            return _modelToAdapter.Map<SupplyRAdapter>(_manager.Read(key));
        }

        public IEnumerable<SupplyRAdapter> GetSupplyList(string categoryKey)
        {
            return _modelToAdapter.Map<IEnumerable<SupplyRAdapter>>(_manager
                .GetProductsOfCategory(categoryKey));
        }

        public bool SetAmount(IEnumerable<SupplyRAdapter> adapters)
        {
            return _manager.SetAmount(_pAToModel.Map<IEnumerable<SupplyModel>>(adapters));
        }

        public bool CreateAmount(SupplyWAdapter adapter)
        {
            return _manager.CheckProp(adapter.AttachedProduct!)
                || _manager.CreateAmount(_fAToModel.Map<SupplyModel>(adapter));
        }

        public bool PreserveAmount(string key, int amount)
        {
            SupplyModel model = _manager.Track(key)!;

            return (amount <= model.Amount) ? _manager.Decrease(model, amount) : false;
        }

        public bool RestoreAmount(string key, int amount)
        {
            SupplyModel model = _manager.Track(key)!;

            return _manager.Increase(model, amount);
        }
    }
}
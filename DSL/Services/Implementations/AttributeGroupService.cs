using AutoMapper;

using BLL.Managers.Declarations;

using DAL.Models;

using DSL.Adapters.Attribute;
using DSL.Adapters.Group;
using DSL.Services.Declarations;

namespace DSL.Services.Implementations
{
    public class AttributeGroupService(IAttributeGroupManager manager)
        : IAttributeGroupService
    {
        private readonly IAttributeGroupManager _manager = manager;

        private readonly Mapper _rwAToModel = new(new MapperConfiguration(cfg
            => cfg.CreateMap<AttributeGroupRWAdapter, AttributeGroupModel>()));

        private readonly Mapper _modelToRWA = new(new MapperConfiguration(cfg
            => cfg.CreateMap<AttributeGroupModel, AttributeGroupRWAdapter>()));
        private readonly Mapper _modelToRA = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AttributeGroupModel, AttributeGroupRAdapter>()
            .ForMember(m => m.AttachedToCategory, opts => opts
            .MapFrom(p => p.CategoryNav!.Category));
        }));

        private readonly Mapper _modelToXRA = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AttributeGroupModel, AttributeGroupXRAdapter>();
            cfg.CreateMap<AttributeModel, ProductAttributeXRWAdapter>();
        }));

        public AttributeGroupRWAdapter GetGroup(string groupKey)
        {
            return _modelToRWA
                .Map<AttributeGroupRWAdapter>(_manager
                .Read(groupKey));
        }

        public IEnumerable<AttributeGroupRWAdapter> GetGroups()
        {
            return _modelToRWA
                .Map<IEnumerable<AttributeGroupRWAdapter>>(_manager
                .ReadAll());
        }

        public IEnumerable<AttributeGroupRWAdapter> GetGroups(IEnumerable<string> groupKeys)
        {
            foreach (var key in groupKeys)
                yield return GetGroup(key);
        }

        public IEnumerable<AttributeGroupXRAdapter> GetGroupsByCategory(string categoryKey)
        {
            return _modelToXRA
                .Map<IEnumerable<AttributeGroupXRAdapter>>(_manager
                .ReadByCategoryKey(categoryKey));
        }

        public IEnumerable<AttributeGroupRAdapter> GetGroupsAsList()
        {
            return _modelToRA
                .Map<IEnumerable<AttributeGroupRAdapter>>(_manager
                .ReadAll());
        }

        public bool PushOrModifyGroup(AttributeGroupRWAdapter adapter)
        {
            return _manager.Has(adapter.Key)
                ? _manager.Update(_rwAToModel.Map<AttributeGroupModel>(adapter))
                : _manager.Create(_rwAToModel.Map<AttributeGroupModel>(adapter));
        }

        public bool DropGroup(string groupKey)
        {
            return _manager.Has(groupKey) &&
                _manager.Delete(_manager.Read(groupKey)!);
        }
    }
}
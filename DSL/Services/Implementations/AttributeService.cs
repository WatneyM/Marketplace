using AutoMapper;

using BLL.Managers.Declarations;

using DAL.Models;

using DSL.Adapters.Attribute;
using DSL.Adapters.Group;
using DSL.Services.Declarations;

namespace DSL.Services.Implementations
{
    public class AttributeService(IAttributeManager manager)
        : IAttributeService
    {
        private readonly IAttributeManager _manager = manager;

        private readonly Mapper _rwAToModel = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AttributeRWAdapter, AttributeModel>();
            cfg.CreateMap<AttributeGroupRWAdapter, AttributeGroupModel>();
        }));

        private readonly Mapper _modelToRWA = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AttributeGroupModel, AttributeGroupRWAdapter>();
            cfg.CreateMap<AttributeModel, AttributeRWAdapter>();
        }));
        private readonly Mapper _modelToRA = new(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AttributeModel, AttributeRAdapter>()
            .ForMember(m => m.AttachedToGroup, opts => opts
            .MapFrom(p => p.GroupNav!.Group));
        }));

        public AttributeRWAdapter GetAttribute(string attributeKey)
        {
            return _modelToRWA
                .Map<AttributeRWAdapter>(_manager
                .Read(attributeKey));
        }

        public IEnumerable<AttributeRWAdapter> GetAttributes()
        {
            return _modelToRWA
                .Map<IEnumerable<AttributeRWAdapter>>(_manager
                .ReadAll());
        }

        public IEnumerable<AttributeRAdapter> GetAttributesAsList()
        {
            return _modelToRA
                .Map<IEnumerable<AttributeRAdapter>>(_manager
                .ReadAll());
        }

        public bool PushOrModifyAttribute(AttributeRWAdapter adapter)
        {
            return _manager.Has(adapter.Key)
                ? _manager.Update(_rwAToModel.Map<AttributeModel>(adapter))
                : _manager.Create(_rwAToModel.Map<AttributeModel>(adapter));
        }

        public bool DropAttribute(string attributeKey)
        {
            return _manager.Has(attributeKey) &&
                _manager.Delete(_manager.Read(attributeKey)!);
        }
    }
}
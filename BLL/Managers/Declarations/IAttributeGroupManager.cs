using DAL.Models;

namespace BLL.Managers.Declarations
{
    public interface IAttributeGroupManager : IModelManager<AttributeGroupModel>
    {
        public IEnumerable<AttributeGroupModel> ReadByCategoryKey(string categoryKey);
    }
}
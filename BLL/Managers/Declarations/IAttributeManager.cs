using DAL.Models;

namespace BLL.Managers.Declarations
{
    public interface IAttributeManager : IModelManager<AttributeModel>
    {
        public IEnumerable<KeyValuePair<string, string>> ReadFiltersNames(string categoryKey);
        public IEnumerable<string> ReadFilters(IEnumerable<string> keys);
        public IEnumerable<KeyValuePair<string, string>> ReadAttributeValues(IEnumerable<string> keys);
    }
}
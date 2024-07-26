using DAL.Models;

namespace BLL.Managers.Declarations
{
    public interface ICategoryManager : IModelManager<CategoryModel>
    {
        public bool HasAttached(string categoryKey);
        public bool HasAttached(string categoryKey, int requiredAmount);

        public IEnumerable<CategoryModel> ReadAttachables();
    }
}
using DAL.Models;

namespace BLL.Managers.Declarations
{
    public interface ICategoryManager : IModelManager<CategoryModel>
    {
        public IEnumerable<CategoryModel> ReadAttachables();
        public IEnumerable<CategoryModel> ReadPrimaries();
    }
}
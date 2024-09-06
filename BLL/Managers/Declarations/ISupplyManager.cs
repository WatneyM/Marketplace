using DAL.Models;

namespace BLL.Managers.Declarations
{
    public interface ISupplyManager
    {
        public bool CheckKey(string key);
        public bool CheckProp(string key);

        public SupplyModel? Track(string dbKey);
        public SupplyModel Read(string dbKey);

        public IEnumerable<SupplyModel> GetProductsOfCategory(string categoryKey);

        public bool SetAmount(IEnumerable<SupplyModel> model);
        public bool CreateAmount(SupplyModel model);

        public bool Increase(SupplyModel model, int amount);
        public bool Decrease(SupplyModel model, int amount);
    }
}
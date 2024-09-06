using DSL.Adapters.Maintenance.Product;

namespace DSL.Services.Declarations
{
    public interface ISupplyService
    {
        public SupplyRAdapter GetSupplyRecord(string key);
        public IEnumerable<SupplyRAdapter> GetSupplyList(string categoryKey);

        public bool SetAmount(IEnumerable<SupplyRAdapter> adapters);
        public bool CreateAmount(SupplyWAdapter adapter);

        public bool PreserveAmount(string key, int amount);
        public bool RestoreAmount(string key, int amount);
    }
}
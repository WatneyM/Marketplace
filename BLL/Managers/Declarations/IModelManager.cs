namespace BLL.Managers.Declarations
{
    public interface IModelManager<TModel> where TModel : class
    {
        public bool Has(string dbKey);

        public int Count();

        public TModel? Track(string dbKey);
        public TModel? Read(string dbKey);
        public IEnumerable<TModel> ReadAll();

        public bool Create(TModel model);
        public bool Update(TModel model);
        public bool Delete(TModel model);
    }
}
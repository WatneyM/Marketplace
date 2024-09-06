using Microsoft.AspNetCore.Http;

namespace DSL.Services.Declarations
{
    public interface ISessionService
    {
        public TEntity? Get<TEntity>(ISession session, string key)
            where TEntity : class;

        public void Set<TEntity>(ISession session, string key, TEntity entity)
            where TEntity : class;
    }
}
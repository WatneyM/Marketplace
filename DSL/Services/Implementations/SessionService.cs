using Microsoft.AspNetCore.Http;
using System.Text.Json;

using DSL.Services.Declarations;

namespace DSL.Services.Implementations
{
    public class SessionService : ISessionService
    {
        public TEntity? Get<TEntity>(ISession session, string key)
            where TEntity : class
        {
            string entity = session.GetString(key);

            return entity == null ? default : JsonSerializer.Deserialize<TEntity>(entity);
        }

        public void Set<TEntity>(ISession session, string key, TEntity entity)
            where TEntity : class
        {
            session.SetString(key, JsonSerializer.Serialize(entity));
        }
    }
}
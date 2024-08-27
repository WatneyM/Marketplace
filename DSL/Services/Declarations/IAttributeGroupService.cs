using DSL.Adapters.Maintenance.Group;

namespace DSL.Services.Declarations
{
    public interface IAttributeGroupService
    {
        public bool KeyCheck(string key);

        public AttributeGroupRWAdapter GetGroup(string groupKey);

        public IEnumerable<AttributeGroupRWAdapter> GetGroups();
        public IEnumerable<AttributeGroupRWAdapter> GetGroups(IEnumerable<string> groupKeys);
        public IEnumerable<AttributeGroupXRAdapter> GetGroupsByCategory(string categoryKey);

        public IEnumerable<AttributeGroupRAdapter> GetGroupsAsList();

        public bool PushOrModifyGroup(AttributeGroupRWAdapter adapter);
        public bool DropGroup(string key);
    }
}
using DSL.Adapters.Maintenance.Attribute;

namespace DSL.Services.Declarations
{
    public interface IAttributeService
    {
        public bool KeyCheck(string key);

        public AttributeRWAdapter GetAttribute(string attributeKey);

        public IEnumerable<AttributeRWAdapter> GetAttributes();

        public IEnumerable<AttributeRAdapter> GetAttributesAsList();

        public bool PushOrModifyAttribute(AttributeRWAdapter adapter);
        public bool DropAttribute(string attributeKey);
    }
}
using DSL.Adapters.Maintenance.Attribute;

namespace DSL.Adapters.Maintenance.Group
{
    public class AttributeGroupXRAdapter
    {
        public string Key { get; set; } = string.Empty;

        public string Group { get; set; } = string.Empty;

        public List<ProductAttributeXRWAdapter> AttachedAttributes { get; set; } = [];
    }
}
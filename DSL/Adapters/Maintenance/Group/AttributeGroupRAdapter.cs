namespace DSL.Adapters.Maintenance.Group
{
    public class AttributeGroupRAdapter
    {
        public string Key { get; set; } = string.Empty;

        public string Group { get; set; } = string.Empty;

        public string AttachedToCategory { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
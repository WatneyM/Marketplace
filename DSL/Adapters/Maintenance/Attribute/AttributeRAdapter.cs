namespace DSL.Adapters.Maintenance.Attribute
{
    public class AttributeRAdapter
    {
        public string Key { get; set; } = string.Empty;

        public string Attribute { get; set; } = string.Empty;
        public bool UseAsFilter { get; set; }

        public string AttachedToGroup { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
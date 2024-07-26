namespace DSL.Adapters.Category
{
    public class CategoryRAdapter
    {
        public string Key { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string? AttachedToCategory { get; set; }

        public bool Attachable { get; set; } = true;

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
namespace DSL.Adapters.Category
{
    public class CategoryRAdapter
    {
        public string Key { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int AttachedProducts { get; set; }

        public string? AttachedToCategory { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
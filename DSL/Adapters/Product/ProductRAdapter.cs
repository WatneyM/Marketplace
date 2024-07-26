namespace DSL.Adapters.Product
{
    public class ProductRAdapter
    {
        public string Key { get; set; } = string.Empty;

        public string Product { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public string AttachedToCategory { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
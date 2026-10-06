namespace BlogPostApi.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UrlHandle { get; set; } = string.Empty;

        // Propiedades opcionales
        public string? Description { get; set; }
        public string? IconUrl { get; set; }

        // Relaciones 

        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();


    }
}

namespace BlogPostApi.Domain
{
    public class BlogPost
    {
        // Identificador único del post (clave primaria)
        public Guid Id { get; set; }

        // Propiedades del contenido del post
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Excerpt { get; set; } = string.Empty;

        // Propiedades opcionales
        public string? FeaturedImageUrl { get; set; }
        public string? Author { get; set; }
        public bool IsVisible { get; set; } = false;
        public DateTime? PublishedDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //  Relaciones 

        // Clave foranea que vincula el post con su categoria
    }
}

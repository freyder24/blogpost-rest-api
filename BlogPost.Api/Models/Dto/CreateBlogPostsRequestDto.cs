namespace BlogPostApi.Models.Dto
{
    public class CreateBlogPostsRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;

        // Propiedades opcionales
        public string? FeaturedImageUrl { get; set; }
        // Imagenes adicionales
        public List<string> Images { get; set; } = new();
        public string? Author { get; set; }
        public bool IsVisible { get; set; } = false;
        public DateTime? PublishedDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Relacion con BlogPost
        public Guid[] Categories { get; set; }

    }
}

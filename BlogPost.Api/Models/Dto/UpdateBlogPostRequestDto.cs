namespace BlogPostApi.Models.Dto
{
    public class UpdateBlogPostRequestDto
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
        //Relacion con BlogPost
        public List<Guid> Categories { get; set; } = new List<Guid>();
    }
}

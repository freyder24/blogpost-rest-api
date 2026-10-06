namespace BlogPostApi.Models.Dto
{
    public class BlogPostDto
    {

        public Guid Id { get; set; }
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

        //Relacion con CategoryDto
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}

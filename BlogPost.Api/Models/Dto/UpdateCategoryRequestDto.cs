namespace BlogPostApi.Models.Dto
{
    public class UpdateCategoryRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string UrlHandle { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }

    }
}

namespace BlogPostApi.Models.Dto
{
    public class CreateImageRequestDto
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
    }
}

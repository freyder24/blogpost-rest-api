using BlogPostApi.Domain;

namespace BlogPostApi.Repositories.Interface
{
    public interface IImageRepository
    {
        Task <Image> Upload(IFormFile file, Image image);
        Task<IEnumerable<Image>> GetAllImagesAsync(); 
    }
}

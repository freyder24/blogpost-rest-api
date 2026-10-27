using BlogPostApi.Domain;

namespace BlogPostApi.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync(); 
        Task<BlogPost?> GetBlogPostByIdAsync(Guid id);
        Task<BlogPost?> GetBlogPostBySlugAsync(string slug);
        Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteBlogPostAsync(Guid id);
    }
}

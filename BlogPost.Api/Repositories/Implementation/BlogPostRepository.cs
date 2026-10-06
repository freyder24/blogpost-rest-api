using BlogPostApi.Data;
using BlogPostApi.Domain;
using BlogPostApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogPostApi.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost)
        {
            
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogPostAsync(Guid id)
        {
            var existingBlogPost = await _dbContext.BlogPosts.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost != null)
            {
                _dbContext.BlogPosts.Remove(existingBlogPost);
                await _dbContext.SaveChangesAsync();
                return existingBlogPost;

            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }


        public Task<BlogPost?> GetBlogPostByIdAsync(Guid id)
        {
            return _dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost)
        {
           var existingBlogPost = await _dbContext.BlogPosts.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }

           
            _dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            var selectedCategories = blogPost.Categories.Select(c => c.Id).ToList();
            existingBlogPost.Categories.Clear();

            var categoriesToAdd = await _dbContext.Categories
                .Where(c => selectedCategories.Contains(c.Id))
                .ToListAsync();

            foreach (var category in categoriesToAdd)
            {
                existingBlogPost.Categories.Add(category);
            }

            await _dbContext.SaveChangesAsync();
            return existingBlogPost;
        }
    }
}

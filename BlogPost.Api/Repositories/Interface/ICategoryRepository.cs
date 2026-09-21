using BlogPostApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostApi.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<IEnumerable<Category>> GetAllCategoryAsync();

        Task<Category?> GetCategoryByIdAsync(Guid id);

        Task<Category?> UpdateCategoryAsync(Category category);

        Task<Category?> DeleteCategoryAsync(Guid id);


    }
}

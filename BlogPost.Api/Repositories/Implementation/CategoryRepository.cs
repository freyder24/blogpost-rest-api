using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPostApi.Data;
using BlogPostApi.Domain;
using BlogPostApi.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogPostApi.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        // Inyeccion de dependencias del ApplicationDbContext
        private readonly ApplicationDbContext _dbContext;

        // Constructor para inyectar el ApplicationDbContext
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

       

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
           var categoryToUpdate = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (categoryToUpdate == null)
            {
                return null;
            }
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.UrlHandle = category.UrlHandle;
            categoryToUpdate.Description = category.Description;
            categoryToUpdate.IconUrl = category.IconUrl;
            await _dbContext.SaveChangesAsync();

            return categoryToUpdate;

        }


        public async Task<Category?> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }
            _dbContext.Categories.Remove(existingCategory);
            await _dbContext.SaveChangesAsync();

            return existingCategory;
        }

       
    }
}

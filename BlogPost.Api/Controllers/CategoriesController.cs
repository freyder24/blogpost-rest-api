using BlogPostApi.Data;
using BlogPostApi.Domain;
using BlogPostApi.Models.Dto;
using BlogPostApi.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostApi.Controllers
{
    // http://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }



        // POST: api/categories
        // http://localhost:xxxx/api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {

            //Mapear dto a modelo dominio
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
                Description = request.Description,
                IconUrl = request.IconUrl


            };

            // Guardar en base de datos
            await _categoryRepository.CreateCategoryAsync(category);


            // Retornar respuesta modelo dominio a dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
                Description = category.Description,
                IconUrl = category.IconUrl
            };

            return Ok(response);

        }


        // GET: api/categories
        // http://localhost:xxxx/api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _categoryRepository.GetAllCategoryAsync();

             var response = new List<CategoryDto>();
            // Mapear modelo dominio a dto
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                    Description = category.Description,
                    IconUrl = category.IconUrl
                });
            }
            return Ok(response);
        }


        // GET: api/categories/{id}
        // http://localhost:xxxx/api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory is null)
            {
                return NotFound();
            }

            // Mapear modelo dominio a dto
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle,
                Description = existingCategory.Description,
                IconUrl = existingCategory.IconUrl
            };

            return Ok(response);
        }

        // GET: api/categories/{id}
        // http://localhost:xxxx/api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto request)
        {
            
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
                Description = request.Description,
                IconUrl = request.IconUrl   
            };

           
            var categoryUpdated = await _categoryRepository.UpdateCategoryAsync(category);
            if (categoryUpdated is null)
            {
                return NotFound();
            }
            return Ok(categoryUpdated);
        }



        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            
            var categoryDeleted = await _categoryRepository.DeleteCategoryAsync(id);
            if (categoryDeleted is null)
            {
                return NoContent();
            }

            // Mapear modelo dominio a dto
            var response = new CategoryDto
            {
                Id = categoryDeleted.Id,
                Name = categoryDeleted.Name,
                UrlHandle = categoryDeleted.UrlHandle,
                Description = categoryDeleted.Description,
                IconUrl = categoryDeleted.IconUrl
            }; 
            return Ok(response);
        }

    }
}

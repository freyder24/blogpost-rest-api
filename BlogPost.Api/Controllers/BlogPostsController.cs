using BlogPostApi.Domain;
using BlogPostApi.Models.Dto;
using BlogPostApi.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {


        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository  _categoryRepository;


        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }


        // POST
        // http://{apiBaseUrl}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostsRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Title = request.Title,
                Slug = request.Slug,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Images = request.Images,
                Author = request.Author,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                //Relacion con categorias
                Categories = new List<Category>()
            };

            //Obtener categorias de la base de datos
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetCategoryByIdAsync(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }


            //Guardar blog post en la base de datos
            await _blogPostRepository.CreateBlogPostAsync(blogPost);


            //Retornar una respuestacmodelo dto
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Slug = blogPost.Slug,
                Content = blogPost.Content,
                ShortDescription = blogPost.ShortDescription,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Images = blogPost.Images,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                    IconUrl = x.IconUrl,
                }).ToList()
            };


            return Ok(response);


        }

        //GET ALL
        // http://{apiBaseUrl}/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await _blogPostRepository.GetAllBlogPostsAsync();

            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    Slug = blogPost.Slug,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Images = blogPost.Images,
                    ShortDescription = blogPost.ShortDescription,
                    //Relacion con categori 
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                        IconUrl = x.IconUrl,
                    }).ToList()
                });
            }

            return Ok(response);
        }


        //GET BY ID
        // http://{apiBaseUrl}/api/blogposts/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.GetBlogPostByIdAsync(id);
            if (blogPost is null)
            {
                return NotFound();
            }
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Slug = blogPost.Slug,
                Author = blogPost.Author,
                Content = blogPost.Content,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Images = blogPost.Images,
                ShortDescription = blogPost.ShortDescription,
                //Relacion con categoria 
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                    IconUrl = x.IconUrl,
                }).ToList()
            };
            return Ok(response);
        }


        //UPDATE BY ID
        // http://{apiBaseUrl}/api/blogposts/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {
            
            var blogPost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                Slug = request.Slug,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Images = request.Images,
                Author = request.Author,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                Categories = new List<Category>()
            };

            //Obtener categorias de la base de datos
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetCategoryByIdAsync(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            var updatedBlogPost = await _blogPostRepository.UpdateBlogPostAsync(blogPost);

            if (updatedBlogPost == null)
            {
                return NotFound();
            }

            //convertir modelo dominio a modelo dto
            var response = new BlogPostDto
            {
                Id = updatedBlogPost.Id,
                Title = updatedBlogPost.Title,
                Slug = updatedBlogPost.Slug,
                Author = updatedBlogPost.Author,
                Content = updatedBlogPost.Content,
                IsVisible = updatedBlogPost.IsVisible,
                PublishedDate = updatedBlogPost.PublishedDate,
                FeaturedImageUrl = updatedBlogPost.FeaturedImageUrl,
                Images = updatedBlogPost.Images,
                ShortDescription = updatedBlogPost.ShortDescription,
                Categories = updatedBlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                    IconUrl = x.IconUrl,
                }).ToList()
            };


            return Ok(response);

        }



        //DELETE
        // http://{apiBaseUrl}/api/blogposts/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var deletedBlogPost = await _blogPostRepository.DeleteBlogPostAsync(id);

            if (deletedBlogPost == null)
            {
                return NotFound();
            }

            //convertir modelo dominio a modelo dto
            var response = new BlogPostDto
            {
                Id = deletedBlogPost.Id,
                Title = deletedBlogPost.Title,
                Slug = deletedBlogPost.Slug,
                Author = deletedBlogPost.Author,
                Content = deletedBlogPost.Content,
                IsVisible = deletedBlogPost.IsVisible,
                PublishedDate = deletedBlogPost.PublishedDate,
                FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
                Images = deletedBlogPost.Images,
                ShortDescription = deletedBlogPost.ShortDescription,
                
            };

            return Ok(response);

        }

    }

}

using BlogPostApi.Domain;
using BlogPostApi.Models.Dto;
using BlogPostApi.Repositories.Implementation;
using BlogPostApi.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        // POST
        //http://{apiBaseUrl}:5125/api/Images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] CreateImageRequestDto request)
        {
            ValidateImageFile(request.File);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Crear el objeto BlogImage
            var blogImage = new Image
            {
                FileExtension = Path.GetExtension(request.File.FileName).ToLower(),
                FileName = request.FileName,
                Title = request.Title,
                CreatedDate = DateTime.UtcNow,
            };

            // Subir usando el repositorio
            blogImage = await _imageRepository.Upload(request.File, blogImage);

            //Convertir modelo dominio a modelo dto
            var response = new ImageDto
            {
                Id = blogImage.Id,
                FileExtension = blogImage.FileExtension,
                FileName = blogImage.FileName,
                Title = blogImage.Title,
                Url = blogImage.Url,
                CreatedDate = blogImage.CreatedDate
            };

            return Ok(response);
        }





        // GET ALL
        //http://{apiBaseUrl}:5125/api/Images
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _imageRepository.GetAllImagesAsync();

            var response = new List<ImageDto>();
            // Mapear modelo dominio a dto
            foreach (var image in images)
            {
                response.Add(new ImageDto
                {
                    Id = image.Id,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Title = image.Title,
                    Url = image.Url,
                    CreatedDate = image.CreatedDate
                });
            }

            return Ok(response);
        }





        private void ValidateImageFile(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                ModelState.AddModelError("file", "No se proporcionó ningún archivo de imagen.");
                return;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                //throw new ArgumentException("El archivo proporcionado no es una imagen válida.");
                ModelState.AddModelError("file", "El archivo proporcionado no es una imagen válida.");
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                ModelState.AddModelError("file", "El archivo no puede ser mayor a 10 MB.");
            }
        }
    }
}
using BlogPostApi.Data;
using BlogPostApi.Domain;
using BlogPostApi.Repositories.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BlogPostApi.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;

        public ImageRepository(
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext
        )
        {
            // campos privados
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Image>> GetAllImagesAsync()
        {
            return await _dbContext.BlogImages.ToListAsync();
        }



        public async Task<Image> Upload(IFormFile file, Image image)
        {
            // Carpeta wwwroot/Images servida por UseStaticFiles, si no existe se crea
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Nombre fisico con GUID para evitar colisiones  el nombre original se guarda en la BD
            var physicalFileName = $"{Guid.NewGuid()}{image.FileExtension}";
            var localPath = Path.Combine(folderPath, physicalFileName);

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // Generar la URL publica de la imagen
            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{physicalFileName}";
            image.Url = urlPath;

            // Guardar en la Base de Datos
            await _dbContext.BlogImages.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }
    }
}

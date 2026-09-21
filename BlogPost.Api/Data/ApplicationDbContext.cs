using BlogPostApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogPostApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        // El constructor recibe la configuracion, cadena de conexion
        // desde el contenedor de dependencias registrado en Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Cada DbSet representa una tabla de la base de datos:
        // "BlogPosts" y "Categories"
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}

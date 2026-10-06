using BlogPostApi.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar las imagenes como una lista de strings serializada en JSON
            modelBuilder.Entity<BlogPost>()
                .Property(x => x.Images)
                .HasConversion(
                    images => JsonSerializer.Serialize(images, (JsonSerializerOptions?)null),
                    images =>
                        string.IsNullOrWhiteSpace(images)
                            ? new List<string>()
                            : JsonSerializer.Deserialize<List<string>>(images, (JsonSerializerOptions?)null) ?? new List<string>()
                );


        }
    }

   
}

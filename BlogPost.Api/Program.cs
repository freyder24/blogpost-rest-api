using BlogPostApi.Data;
using BlogPostApi.Repositories.Implementation;
using BlogPostApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuracion de servicios 

// Registra los controladores como servicios de la API
builder.Services.AddControllers()
    // Ignora las referencias circulares al serializar JSON
    // (p.ej. BlogPost -> Category -> BlogPosts) para evitar errores
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Genera la documentacion del API (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra el contexto de Entity Framework Core y lo configura
// para usar SQL Server con la cadena de conexión "DefaultConnection"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//   Registra la implementacion concreta de ICategoryRepository para que pueda ser inyectada en los controladores
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

var app = builder.Build();




// Middleware pipeline (se ejecuta en orden en cada peticion)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// Middleware de autenticación/authorización
app.UseCors(options =>
{
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
});

app.UseAuthorization();

// Enlaza cada ruta definida en los controladores ([Route], [HttpGet], etc.)
app.MapControllers();

// Inicia el servidor y queda a la espera de peticiones
app.Run();

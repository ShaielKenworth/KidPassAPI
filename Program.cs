using EscuelaAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura el DbContext
builder.Services.AddDbContext<EscuelaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar Swagger para la documentación de la API
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Añadir servicios para controladores
builder.Services.AddControllers();

var app = builder.Build();

// Habilitar CORS
app.UseCors("AllowAllOrigins");

// Habilitar Swagger en la aplicación
app.UseSwagger();
app.UseSwaggerUI();  // Esta línea permite que puedas interactuar con la API desde el navegador

// Mapear los controladores
app.MapControllers();

app.Run();

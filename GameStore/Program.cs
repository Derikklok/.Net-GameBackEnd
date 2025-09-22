
using GameStore.Data;
using GameStore.Endpoints;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// MySQL Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add Db Context
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React dev server
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configure JSON serialization to handle circular references
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowReactApp");

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", () => "This is a test endpoint");
app.MapGamesEndpoints();
app.MapControllers(); // Add this line to enable controller routing

app.Run();

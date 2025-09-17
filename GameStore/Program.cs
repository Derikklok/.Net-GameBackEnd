
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

// Configure JSON serialization to handle circular references
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", () => "This is a test endpoint");
app.MapGamesEndpoints();
app.MapControllers(); // Add this line to enable controller routing

app.Run();

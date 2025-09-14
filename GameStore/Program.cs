using GameStore.Data;
using GameStore.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MySQL Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add Db Context
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGamesEndpoints();

app.Run();

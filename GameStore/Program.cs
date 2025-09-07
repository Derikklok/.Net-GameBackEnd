using System.Net.WebSockets;
using GameStore.DTOs;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGamesEndpoints();

app.Run();

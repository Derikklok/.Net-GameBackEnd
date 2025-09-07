using System;
using GameStore.DTOs;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
    new(
        1,
        "Street Fighter 2",
        "Action",
        20.46M,
        new DateOnly(1992,7,23)
    ),
    new(
        2,
        "Mortal Combat",
        "Fiction",
        45.96M,
        new DateOnly(2002,4,12)
    ),
    new(
        3,
        "Gran Turismo 7",
        "Racing",
        74.69M,
        new DateOnly(2023,4,28)
    )
];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        group.MapGet("/", () => games);

        // If no value is available for an id then it output -> null
        group.MapGet("/{id}", (int id) =>
        {

            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }

            ).WithName(GetGameEndpointName);

        // POST
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
                );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);

        });


        // Update games - if you do not provide all the values values will be resetted.
        // We can not update only one single value given in the body of the request
        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                 id,
                 updateGame.Name,
                 updateGame.Genre,
                 updateGame.Price,
                 updateGame.ReleaseDate
            );

            return Results.NoContent();
        });


        //  Delete games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}

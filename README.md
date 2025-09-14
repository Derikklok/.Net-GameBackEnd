# Game Store API

A RESTful API for managing video games and tasks built with ASP.NET Core 9.0.

## Table of Contents

- [Setup & Installation](#setup--installation)
- [Database Configuration](#database-configuration)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
  - [Basic Endpoints](#basic-endpoints)
  - [Game Endpoints](#game-endpoints)
  - [Task Endpoints](#task-endpoints)
  - [Test Endpoints](#test-endpoints)

## Setup & Installation

### Prerequisites

- .NET 9.0 SDK
- MySQL Server

### Clone the Repository

```bash
git clone https://github.com/Derikklok/.Net-GameBackEnd.git
cd GameStore
```

### Install Dependencies

```bash
dotnet restore
```

## Database Configuration

The application is configured to use MySQL. The connection string is defined in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=taskmanagerdb;user=root;password=sachin1605"
  }
}
```

### Set Up the Database

1. Install the EF Core tools if you haven't already:

```bash
dotnet tool install --global dotnet-ef
```

2. Create the database and apply migrations:

```bash
dotnet ef database update
```

## Running the Application

```bash
dotnet run
```

The API will be available at `http://localhost:5251`.

## API Endpoints

### Basic Endpoints

| Method | Endpoint | Description | Example Request | Example Response |
|--------|----------|-------------|----------------|------------------|
| GET    | `/`      | Returns a hello message | `GET /` | `"Hello World!"` |
| GET    | `/test`  | Returns a test message | `GET /test` | `"This is a test endpoint"` |

### Game Endpoints

| Method | Endpoint | Description | Example Request | Example Response |
|--------|----------|-------------|----------------|------------------|
| GET    | `/games` | Returns a list of all games | `GET /games` | `[{"id":1,"name":"Street Fighter 2","genre":"Action","price":20.46,"releaseDate":"1992-07-23"},...]` |
| GET    | `/games/{id}` | Returns a specific game by ID | `GET /games/1` | `{"id":1,"name":"Street Fighter 2","genre":"Action","price":20.46,"releaseDate":"1992-07-23"}` |
| POST   | `/games` | Creates a new game | `POST /games` <br> Body: `{"name":"Tetris","genre":"Puzzle","price":9.99,"releaseDate":"1984-06-06"}` | `{"id":4,"name":"Tetris","genre":"Puzzle","price":9.99,"releaseDate":"1984-06-06"}` |
| PUT    | `/games/{id}` | Updates a specific game | `PUT /games/1` <br> Body: `{"name":"Street Fighter II Turbo","genre":"Fighting","price":24.99,"releaseDate":"1992-07-23"}` | `204 No Content` |
| DELETE | `/games/{id}` | Deletes a specific game | `DELETE /games/1` | `204 No Content` |

### Task Endpoints

| Method | Endpoint | Description | Example Request | Example Response |
|--------|----------|-------------|----------------|------------------|
| GET    | `/api/tasks` | Returns a list of all tasks | `GET /api/tasks` | `[{"id":1,"title":"Learn ASP.NET","description":"Understand Web API basics","isCompleted":false},...]` |
| GET    | `/api/tasks/{id}` | Returns a specific task by ID | `GET /api/tasks/1` | `{"id":1,"title":"Learn ASP.NET","description":"Understand Web API basics","isCompleted":false}` |
| POST   | `/api/tasks` | Creates a new task | `POST /api/tasks` <br> Body: `{"title":"Learn Entity Framework","description":"Study database migrations","isCompleted":false}` | `{"id":2,"title":"Learn Entity Framework","description":"Study database migrations","isCompleted":false}` |
| PUT    | `/api/tasks/{id}` | Updates a specific task | `PUT /api/tasks/1` <br> Body: `{"id":1,"title":"Learn ASP.NET Core","description":"Master Web API basics","isCompleted":true}` | `204 No Content` |
| DELETE | `/api/tasks/{id}` | Deletes a specific task | `DELETE /api/tasks/1` | `204 No Content` |

### Test Endpoints

| Method | Endpoint | Description | Example Request | Example Response |
|--------|----------|-------------|----------------|------------------|
| GET    | `/api/test` | Test endpoint that confirms controller routing | `GET /api/test` | `"Test controller working!"` |
| POST   | `/api/test` | Test endpoint for POST requests | `POST /api/test` <br> Body: `{"message":"Hello API"}` | `"Received: Hello API"` |

## Models

### Game Model

```csharp
public record class GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
```

### Task Model

```csharp
public class TaskItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsCompleted { get; set; }
}
```

## Notes for Developers

- The game endpoints use minimal API syntax with a MapGroup approach
- The task endpoints use the traditional controller-based approach
- The application uses Entity Framework Core for data persistence with MySQL
- Game data is currently stored in-memory (list), while task data is persisted to the database

## Troubleshooting

### Common Issues

1. **Database Connection Errors**
   - Ensure MySQL is running
   - Verify the connection string in appsettings.json
   - Make sure the database exists

2. **Migration Errors**
   - If you encounter migration issues, try:
     ```bash
     dotnet ef migrations remove
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

3. **404 Errors on API Endpoints**
   - Ensure the application is running
   - Check that you're using the correct URL and HTTP method
   - Verify that controller routing is enabled in Program.cs (`app.MapControllers()`)
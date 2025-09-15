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
  - [Citizens Endpoints](#citizens-endpoints)
  - [Officers Endpoints](#officers-endpoints)
- [Architecture Overview](#architecture-overview)

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

The application is configured to use MySQL. The connection string should be set in your `appsettings.json` file.

### Setting up your appsettings.json

1. Copy the template file:
```bash
cp GameStore/appsettings.template.json GameStore/appsettings.json
```

2. Edit the `appsettings.json` file with your database credentials:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=taskmanagerdb;user=YOUR_USERNAME;password=YOUR_PASSWORD"
  }
}
```

> **Security Note:** The `appsettings.json` file is excluded from version control to protect sensitive information. Never commit this file with actual credentials.

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

### Review Model

```csharp
public class Review
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public required string UserName { get; set; }
    public required string Comment { get; set; }
    public int Rating { get; set; } // 1-5 star rating
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
```

## Notes for Developers

- The game endpoints use minimal API syntax with a MapGroup approach
- The task endpoints use the traditional controller-based approach
- The application uses Entity Framework Core for data persistence with MySQL
- Game data is currently stored in-memory (list), while task data is persisted to the database

## Database Migrations

### Adding a New Model

When you want to add a new model to your application and update the database schema, follow these steps:

1. **Create a new model class** in the `Models` folder
2. **Add a DbSet property** in `AppDbContext.cs`
3. **Create and apply a migration** using Entity Framework Core commands

#### Example: Adding a Review Model

```csharp
// 1. Create Models/Review.cs
public class Review
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public required string UserName { get; set; }
    public required string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}

// 2. Update AppDbContext.cs
public class AppDbContext : DbContext
{
    // ...existing code...
    public DbSet<Review> Reviews { get; set; }
}
```

3. **Create and apply the migration**:

```bash
# Generate migration files
dotnet ef migrations add AddReviewsTable

# Apply migration to database
dotnet ef database update
```

### Common Migration Commands

```bash
# List all migrations
dotnet ef migrations list

# Create a new migration
dotnet ef migrations add [MigrationName]

# Apply pending migrations
dotnet ef database update

# Revert to a specific migration
dotnet ef database update [MigrationName]

# Remove the last migration (if not applied)
dotnet ef migrations remove

# Generate SQL script (without applying)
dotnet ef migrations script

# Drop and recreate the database
dotnet ef database drop --force
dotnet ef database update
```

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

### Citizens Endpoints

| Method | Endpoint | Description | Example Request | Example Response |
|--------|----------|-------------|----------------|------------------|
| GET    | `/api/citizens` | Returns a list of all citizens with their applications | `GET /api/citizens` | `[{"id":1,"fullName":"John Doe","nic":"123456789X","address":"123 Main St","contactNumber":"555-1234","applications":[...]},...]` |
| GET    | `/api/citizens/{id}` | Returns a specific citizen by ID | `GET /api/citizens/1` | `{"id":1,"fullName":"John Doe","nic":"123456789X","address":"123 Main St","contactNumber":"555-1234","applications":[...]}` |
| POST   | `/api/citizens` | Creates a new citizen | `POST /api/citizens` <br> Body: `{"fullName":"Jane Smith","nic":"987654321Y","address":"456 Oak St","contactNumber":"555-5678"}` | `{"id":2,"fullName":"Jane Smith","nic":"987654321Y","address":"456 Oak St","contactNumber":"555-5678","applications":[]}` |
| PUT    | `/api/citizens/{id}` | Updates a specific citizen | `PUT /api/citizens/1` <br> Body: `{"fullName":"John Smith"}` | `{"id":1,"fullName":"John Smith","nic":"123456789X","address":"123 Main St","contactNumber":"555-1234","applications":[...]}` |
| DELETE | `/api/citizens/{id}` | Deletes a specific citizen | `DELETE /api/citizens/1` | `204 No Content` |

### Officers Endpoints

| Method | Endpoint | Description | Example Request | Example Response |
|--------|----------|-------------|----------------|------------------|
| GET    | `/api/officers` | Returns a list of all officers with their assigned applications | `GET /api/officers` | `[{"id":1,"fullName":"Prakash Jayweera","department":"Accounts","applications":[...]},...]` |
| GET    | `/api/officers/{id}` | Returns a specific officer by ID | `GET /api/officers/1` | `{"id":1,"fullName":"Prakash Jayweera","department":"Accounts","applications":[...]}` |
| POST   | `/api/officers` | Creates a new officer | `POST /api/officers` <br> Body: `{"fullName":"Prakash Jayweera","department":"Accounts"}` | `{"id":1,"fullName":"Prakash Jayweera","department":"Accounts","applications":[]}` |
| PUT    | `/api/officers/{id}` | Updates a specific officer | `PUT /api/officers/1` <br> Body: `{"department":"Finance"}` | `{"id":1,"fullName":"Prakash Jayweera","department":"Finance","applications":[...]}` |
| DELETE | `/api/officers/{id}` | Deletes a specific officer | `DELETE /api/officers/1` | `204 No Content` |

## Architecture Overview

### Data Transfer Objects (DTOs)

This project uses DTOs to separate the API contract from the internal data models. This provides several benefits:

1. **Data Protection**: Prevents exposing sensitive fields or internal implementation details
2. **Flexibility**: Allows the API contract to evolve independently from the database schema
3. **Validation**: Enables specific validation rules for different operations
4. **Optimization**: Reduces data transfer by only including necessary fields

#### DTO Types:

1. **Response DTOs** (e.g., `CitizenDto`, `OfficerDto`): Used for sending data to clients
2. **Create DTOs** (e.g., `CreateCitizenDto`, `CreateOfficerDto`): Used for creating new resources
3. **Update DTOs** (e.g., `UpdateCitizenDto`, `UpdateOfficerDto`): Used for updating existing resources with nullable properties

### Entity Framework Core with Repository Pattern

The application uses Entity Framework Core as its ORM (Object-Relational Mapper) with the Repository Pattern:

1. **DbContext**: `AppDbContext` manages the database connection and entity sets
2. **Models**: Define the structure of database tables
3. **Controllers**: Handle HTTP requests and use the DbContext to perform CRUD operations
4. **Migrations**: Track and apply database schema changes

### RESTful API Design

The API follows RESTful conventions:

1. **Resources**: Represented by nouns (citizens, officers, applications)
2. **HTTP Methods**: Used appropriately (GET, POST, PUT, DELETE)
3. **Status Codes**: Standard HTTP status codes (200, 201, 204, 400, 404, etc.)
4. **Relationships**: Represented through nested resources and navigation properties

### Key C# Features Used

1. **Async/Await**: All database operations use asynchronous programming
2. **Dependency Injection**: Services are injected into controllers
3. **Entity Navigation Properties**: Define relationships between entities
4. **Nullable Reference Types**: Used in Update DTOs to indicate optional fields
5. **Records** (for some DTOs): Immutable data transfer objects
6. **Static Methods**: Used for mapping between entities and DTOs
# Booking Hotel API

A backend REST API for managing hotel rooms, customer bookings, and user authentication.

This project was developed with ASP.NET Core and follows Clean Architecture principles. It demonstrates how to build a maintainable backend application with authentication, database access, validation, dependency injection, and clear separation of responsibilities.

## Project Overview

The Booking Hotel API allows users to:

* Register and log in
* View available rooms
* Create new bookings
* Prevent overlapping reservations
* View booking details
* Update or cancel bookings
* Manage rooms
* Access protected endpoints using JWT authentication

The project focuses mainly on backend development and API design.

## Technologies

* C#
* ASP.NET Core Web API
* .NET 8
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* JWT Authentication
* Swagger / OpenAPI
* FluentValidation
* Dependency Injection
* Git

## Architecture

The application is structured according to Clean Architecture principles.

```text
BookingHotel
│
├── BookingHotel.API
│   ├── Controllers
│   ├── Middleware
│   └── Program.cs
│
├── BookingHotel.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Validators
│
├── BookingHotel.Domain
│   ├── Entities
│   └── Common
│
└── BookingHotel.Infrastructure
    ├── Data
    ├── Identity
    ├── Repositories
    └── Migrations
```

### Domain Layer

Contains the main business entities and core business rules.

Main entities include:

* Room
* Booking
* ApplicationUser

### Application Layer

Contains application logic, DTOs, service interfaces, validation rules, and use cases.

### Infrastructure Layer

Contains Entity Framework Core configuration, SQL Server database access, repositories, migrations, Identity, and JWT implementation.

### API Layer

Contains controllers, middleware, dependency injection configuration, Swagger configuration, and API endpoints.

## Main Features

### Authentication and Authorization

The API uses ASP.NET Core Identity and JWT bearer authentication.

Users can register and log in. After a successful login, the API returns a JWT access token that can be used to access protected endpoints.

### Room Management

The application supports operations such as:

* Creating rooms
* Updating room information
* Deleting rooms
* Viewing room details
* Viewing all available rooms

### Booking Management

Users can:

* Create a booking
* View booking details
* View their bookings
* Update a booking
* Cancel a booking

### Booking Conflict Prevention

Before creating a reservation, the application checks whether the selected room is already booked during the requested period.

A booking is rejected when its dates overlap with an existing reservation.

Example overlap rule:

```csharp
newBooking.CheckInDate < existingBooking.CheckOutDate &&
newBooking.CheckOutDate > existingBooking.CheckInDate
```

## API Endpoints

The exact endpoints may depend on the current project version.

### Authentication

```http
POST /api/auth/register
POST /api/auth/login
```

### Rooms

```http
GET    /api/rooms
GET    /api/rooms/{id}
POST   /api/rooms
PUT    /api/rooms/{id}
DELETE /api/rooms/{id}
```

### Bookings

```http
GET    /api/bookings
GET    /api/bookings/{id}
POST   /api/bookings
PUT    /api/bookings/{id}
DELETE /api/bookings/{id}
```

## Getting Started

### Prerequisites

Make sure the following tools are installed:

* .NET 8 SDK
* SQL Server or SQL Server Express
* Visual Studio 2022, JetBrains Rider, or Visual Studio Code
* Git

### Clone the Repository

```bash
git clone https://github.com/Askarimina/BookingHotelSystem.git
cd BookingHotelSystem
```

https://github.com/Askarimina/BookingHotelSystem with your GitHub username.

### Configure the Database

Open the `appsettings.json` file inside the API project and update the SQL Server connection string.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}
```

For SQL Server authentication, you can use:

```json
{
   "ConnectionStrings": {
    "DefaultConnection": ""
  }

}
```

Do not commit production passwords or secret keys to GitHub.

### Configure JWT

Add the JWT settings to `appsettings.json`.

```json
{

  "Jwt": {
    "Key": "",
    "Issuer": "BookingHotel.API",
    "Audience": "BookingHotel.Client",
    "ExpiryMinutes": 60
  }
}
```

For a real production application, JWT secrets should be stored securely using environment variables, Azure Key Vault, or .NET User Secrets.

### Apply Database Migrations

Run the following command from the solution directory:

```bash
dotnet ef database update --project BookingHotel.Infrastructure --startup-project BookingHotel.API
```
 

If the Entity Framework CLI is not installed, install it with:

```bash
dotnet tool install --global dotnet-ef
```

### Run the Application

```bash
dotnet restore
dotnet build
dotnet run --project BookingHotel.API
```

After the application starts, open Swagger in your browser.

```text
https://localhost:xxxx/swagger
```

The exact port number is displayed in the terminal.

## Testing the Authentication

1. Open Swagger.
2. Use the register endpoint to create a user.
3. Use the login endpoint to receive a JWT token.
4. Copy the token.
5. Click the **Authorize** button in Swagger.
6. Enter the token in this format:

```text
Bearer your-jwt-token
```

7. You can now test protected endpoints.

## Example Booking Request

```json
{
  "roomId": 1,
  "checkInDate": "2026-08-10",
  "checkOutDate": "2026-08-15"
}
```

## Validation

The application validates incoming requests before executing business logic.

Examples of validation rules include:

* Room ID must be valid
* Check-in date must be before check-out date
* Required fields cannot be empty
* Email addresses must have a valid format
* A room cannot be booked during an already reserved period

## Error Handling

The API returns appropriate HTTP status codes, such as:

* `200 OK`
* `201 Created`
* `400 Bad Request`
* `401 Unauthorized`
* `404 Not Found`
* `409 Conflict`
* `500 Internal Server Error`

## Future Improvements

Planned improvements include:

* Global exception-handling middleware
* Pagination, filtering, and sorting
* Role-based authorization for administrators and customers
* Integration tests
* Unit tests
* Docker support
* Azure deployment
* Refresh tokens
* Email booking confirmation
* Room image upload
* Logging with Serilog
* CI/CD with GitHub Actions

## What I Learned

While developing this project, I improved my knowledge of:

* Building RESTful APIs with ASP.NET Core
* Applying Clean Architecture
* Working with Entity Framework Core
* Designing relational database models
* Implementing JWT authentication
* Using ASP.NET Core Identity
* Applying dependency injection
* Separating business logic from infrastructure
* Validating API requests
* Preventing overlapping reservations
* Using Git and GitHub for version control

## Project Status

This project is under active development and is used to improve and demonstrate my backend development skills.

## Author

**Mahboubeh Askari**

Backend Developer focused on C#, ASP.NET Core, REST APIs, SQL Server, and Clean Architecture.

* GitHub: https://github.com/Askarimina/BookingHotelSystem
* LinkedIn: https://www.linkedin.com/in/mahboubeh-askari-b243a2226/

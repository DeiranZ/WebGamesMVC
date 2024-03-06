# What is it?
A sample **ASP.NET Core MVC** application that is an online HTML web games site.
It is made following **CQRS** and **Clean Architecture design**.

It uses:
  - EntityFramework for ORM
  - AutoMapper for mapping
  - Xunit and FluentAssertions for testing
  - FluentValidation for validations
  - MediatR for CQRS
  - ASP.NET Core Identity for users, roles etc.
  - toastr.js for notifications
  - screenfull for cross-browser fullscreen

It is currently running ASP.NET Core 8.0.

# Current state

The web app supports both **uploading** `Game` files to the web environment, and **embedding** games from other websites. The same goes for game thumbnails.

Each game can have multiple `Genres` (many to many), that the games can be **filtered** by.

When playing the game, there is a **Fullscreen button** that works regardless of the game supporting the feature.

Users' ability to modify the website content depends on their **role** (admins and moderators can add, edit and delete games).

The repositories support **caching** results to optimize performance.

There are unit and integration tests written for critical functionality.

# Defaults

By default, the app ships with a sample game, several user roles and an admin account that are seeded to the database.

Default admin credentials:
  - email: admin@admin.com
  - password: Admin#21

Default roles:
  - admin
  - moderator
  - user

# Installation

1. If you haven't already, install `dotnet-ef` CLI tool using the following command: 
```
dotnet tool install --global dotnet-ef
```

2. Ensure your connection strings in appsettings.json point to a local SQL Server instance.

3. Update the database by using the following command in the main `WebGames` folder:
```
dotnet ef database update --project ".\WebGames.Infrastructure\" --startup-project ".\WebGames.MVC\"
```

4. You should be able to run the app now.

# Screens

### Index
![The website's index, as seen by a regular user](https://github.com/DeiranZ/WebGamesMVC/assets/40063902/2793eed3-1420-4515-aadb-b7afd1e81330)

### Game creation
![The game creation view](https://github.com/DeiranZ/WebGamesMVC/assets/40063902/e66e04ec-da83-4bb5-acce-187cb406026f)

### Game editing
![Game editing view](https://github.com/DeiranZ/WebGamesMVC/assets/40063902/c643a53c-da47-4734-9a4c-bf2db7f6c7d0)

### Playing the example game
![Play view](https://github.com/DeiranZ/WebGamesMVC/assets/40063902/37fed2f3-b6e1-40b1-abee-38ddcad87b18)

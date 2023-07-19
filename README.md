# Game Server Repository

This repository contains the codebase for the Game Server project, including the hosting service and related infrastructure. The Game Server is designed to handle player interactions and manage gifts and resources in the game.

## Hosts\GameService.WebApi.Host

The `Hosts\GameService.WebApi.Host` directory contains the source code for the web API host that runs the Game Server. To start the server, navigate to this directory and execute the following command:



The server will start running, and you can interact with it using the provided endpoints.

## Infrastructure

### Persistence

The Game Server uses Sqlite as its database for persistence, and Entity Framework Core (EF Core) as the ORM (Object-Relational Mapping) tool.

### Dotnet Version

The project is built using Dotnet version 6.0.

## Console App for Testing

The `GameService.PlayerApp\bin\Debug\net6.0\GameService.PlayerApp.exe` is a console application designed for testing the Game Server functionalities. You can use this application to execute commands and observe the server's responses.

### Commands in Console APP

1) **Login:** To log in as a player, use the `login` command followed by the `deviceId`, which should be an alphanumeric value.

   Example: login 0123456789abcdef0123456789abcdef01234567



2) **Sending Gift:** To send a gift to a player, use the `gift` command followed by the `playerId`, `ResourceType`, and `amount`.

Example: gift 0f288746-d5da-48df-97fd-67d1bca95067 Coins 5

3) **Logout** Logouts current player.

## Further Improvements

The project can benefit from the following improvements:

1) **Changing Player State in a Transaction:** Implementing a transactional mechanism for changing player state would ensure the integrity of the data during state updates.

2) **Better Connection Handling:** Instead of abruptly aborting existing connections and making new connections, consider using existing connections for players who are already connected to enhance connection management.

3) **Unit & Integration Testing:** Implement comprehensive unit and integration tests to ensure the correctness and reliability of the Game Server. This will help identify and fix issues before deployment.

Please feel free to contribute to this project by suggesting improvements, reporting issues, or submitting pull requests. Happy gaming!

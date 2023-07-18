using GameService.Domain.Enums;
using GameService.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class Program
{
    static async Task Main(string[] args)
    {

        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:5001/api/v1/game")
            .Build();

        await connection.StartAsync();

        #region callBacks

        connection.On<string>("ReceiveLogEvent", (message) =>
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        });

        connection.On<PlayerAlreadyConnectedResponse>("PlayerAlreadyConnected", async obj =>
        {
            Console.WriteLine($"Player with DeviceId '{obj.DeviceId}' is already connected.\n " +
                              $"PlayerId : {obj.PlayerId}");

            // Handle if console window reopened.
            if (connection.ConnectionId != obj.ConnectionId) 
            {
                await ReConnect(connection, obj.ConnectionId);
            }
        });
       
        connection.On<string>("PlayerLogin", playerId =>
        {
            Console.WriteLine($"new login : {playerId}");
        });

        connection.On<GiftSentResponse>("GiftSent", obj =>
        {
            Console.WriteLine($"{obj.DeviceId} sent {obj.Amount} {obj.ResourceType} to you.");
        });

        connection.On<string>("PlayerIsOffline", playerId =>
        {
            Console.WriteLine($"Player with Id {playerId} is offline now.");
        });

        connection.On<BalanceIsNotAvailableResponse>("BalanceIsNotAvailable", obj =>
        {
            Console.WriteLine($"You don't have {obj.Amount} {obj.ResourceType} in your wallet.");
        });

        #endregion

        Console.CancelKeyPress += async (_, e) =>
        {
            // It works with ctrl+c
            // Added "logout" command additionally.
            await Logout(connection);
            e.Cancel = false;
        };

        while (true)
        {
            Console.WriteLine("\nPlease enter a command:");

            var command = Console.ReadLine();
            await ProcessCommand(connection, command);
        }
    }

    private static async Task ProcessCommand(HubConnection connection, string command)
    {
        string[] parts = command.Split(' ');
        string action = parts[0].ToLower();

        switch (action)
        {
            case "login":

                string deviceId = parts[1];
                await Login(connection, deviceId);
                break;

            case "logout":
                await Logout(connection);
                break;

            case "gift":

                if (parts.Length < 4)
                {
                    Console.WriteLine("Invalid command format. Please provide FriendPlayerId, ResourceType, and Amount.");
                    break;
                }

                var friendPlayerId = parts[1];
                var resourceTypeString = Enum.Parse<ResourceType>(parts[2]);
                var amount = int.Parse(parts[3]);

                await SendGift(connection, friendPlayerId, resourceTypeString, amount);
                break;

            default:
                Console.WriteLine("Invalid command. Please try again.");
                break;
        }
    }

    private static async Task SendGift(HubConnection connection, string friendPlayerId, ResourceType resourceType, int amount)
    {
        try
        {
            await connection.InvokeAsync("SendGift", friendPlayerId, resourceType, amount);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send gift. Error: " + ex.Message);
        }
    }

    private static async Task Login(HubConnection connection, string deviceId)
    {
        try
        {
            await connection.InvokeAsync("Login", deviceId);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Login failed. Error: " + ex.Message);
        }
    }

    private static async Task ReConnect(HubConnection connection, string existingConnectionId)
    {
        try
        {
            await connection.InvokeAsync("ReConnect", existingConnectionId, connection.ConnectionId);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Login failed. Error: " + ex.Message);
        }
    }


    private static async Task Logout(HubConnection connection)
    {
        try
        {
            await connection.InvokeAsync("Logout");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Logout failed. Error: " + ex.Message);
        }
    }
}

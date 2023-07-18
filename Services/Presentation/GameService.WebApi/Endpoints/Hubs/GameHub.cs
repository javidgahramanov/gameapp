using GameService.Domain.Entities;
using GameService.Domain.Enums;
using GameService.Domain.Models;
using GameService.Service.Contracts.Services;
using GameService.WebApi.Constants;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.WebApi.Endpoints.Hubs
{
    public class GameHub : Hub
    {
        private readonly ILogger<GameHub> _logger;
        HashSet<HubCallerContext> hubConnections = new HashSet<HubCallerContext>();

        private readonly IPlayerService _playerService;
        private readonly PlayerManager _playerManager = new();

        public GameHub(IPlayerService playerService, ILogger<GameHub> logger)
        {
            _playerService = playerService;
            _logger = logger;
        }
        public override Task OnConnectedAsync()
        {
            hubConnections.Add(Context);
            return base.OnConnectedAsync();
        }

        public async Task Login(string deviceId)
        {
            var entity = (await _playerService.QueryAsync(c => c.DeviceId == deviceId)).FirstOrDefault();

            var player = entity ?? await _playerService.CreateAsync(new Player { DeviceId = deviceId });

            var playerAlreadyConnected = _playerManager.IsPlayerOnline(player);

            if (playerAlreadyConnected)
            {
                var existingConnectionId = _playerManager.GetConnectionIdByPlayerId(player.Id);

                await Clients.Client(Context.ConnectionId).SendAsync(HubMethodConstants.PlayerAlreadyConnected,

                    new PlayerAlreadyConnectedResponse
                    {
                        ConnectionId = existingConnectionId,
                        DeviceId = player.DeviceId,
                        PlayerId = player.Id
                    });
                _logger.LogInformation("LOG: Login attempt...");
            }
            else
            {
                await Clients.All.SendAsync(HubMethodConstants.PlayerLogin, player.Id);
                _playerManager.AddPlayer(player, Context.ConnectionId);
                _logger.LogInformation("LOG: Successful login...");
            }
        }

        public Task Logout()
        {
            var player = _playerManager.GetPlayer(Context.ConnectionId);

            Console.WriteLine($"Player {player.DeviceId} left game.");
            _logger.LogInformation($"Player {player.DeviceId} left game.");

            _playerManager.RemovePlayer(Context.ConnectionId);

            return Task.CompletedTask;
        }

        public Task ReConnect(string existingConnectionId, string newConnectionId)
        {
            var player = _playerManager.GetPlayer(existingConnectionId);
            _playerManager.UpdateConnection(existingConnectionId, newConnectionId, player);

            var connectionToBeAborted = hubConnections.FirstOrDefault(c => c.ConnectionId == existingConnectionId);

            connectionToBeAborted?.Abort();
            return Task.CompletedTask;
        }

        public async Task SendGift(string friendPlayerId, ResourceType resourceType, int amount)
        {
            var senderId = _playerManager.GetPlayer(Context.ConnectionId).Id;
            var senderPlayer = await _playerService.FindAsync(senderId);

            if (!senderPlayer.BalanceIsAvailableForGit(resourceType, amount))
            {
                await Clients.Client(Context.ConnectionId).SendAsync(HubMethodConstants.BalanceIsNotAvailable,
                    new BalanceIsNotAvailableResponse { Amount = amount, ResourceType = resourceType });
                _logger.LogInformation("LOG: Insufficient Balance.");
            }
            else
            {
                var friendPlayer = await _playerService.FindAsync(Guid.Parse(friendPlayerId));

                if (friendPlayer is null)
                {
                    _logger.LogError("LOG: Player Not Found...");
                    throw new Exception("Player not found!");
                }

                var playerIsOnline = _playerManager.IsPlayerOnline(friendPlayer);

                if (playerIsOnline)
                {
                    senderPlayer.SendGift(friendPlayer, resourceType, amount);

                    await _playerService.UpdateAsync(friendPlayer);
                    await _playerService.UpdateAsync(senderPlayer);

                    await Clients.Client(_playerManager.GetConnectionIdByPlayerId(friendPlayer.Id))
                        .SendAsync(HubMethodConstants.GiftSent, new GiftSentResponse
                        {
                            DeviceId = senderPlayer.DeviceId,
                            Amount = amount,
                            ResourceType = resourceType
                        });

                    _logger.LogError("LOG: Sending gift...");
                }
                else
                {
                    await Clients.Others.SendAsync(HubMethodConstants.PlayerIsOffline, friendPlayerId);
                }
            }
        }
    }
}

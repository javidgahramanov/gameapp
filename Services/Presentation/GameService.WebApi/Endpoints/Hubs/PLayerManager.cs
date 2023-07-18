using System;
using GameService.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameService.WebApi.Endpoints.Hubs
{
    public class PlayerManager
    {
        static readonly Dictionary<string, Player> _players = new();

        public void AddPlayer(Player player, string connectionId)
        {
            _players[connectionId] = player;
        }

        public void RemovePlayer(string connectionId)
        {
            _players.Remove(connectionId);
        }

        public bool IsPlayerOnline(Player player)
        {
            return _players.Any(c => c.Value.DeviceId == player.DeviceId);
        }

        public Player GetPlayer(string connectionId)
        {
            return _players[connectionId];
        }

        public string GetConnectionIdByPlayerId(Guid playerId)
        {
            var player = _players.FirstOrDefault(c => c.Value.Id == playerId);
            return player.Value != null ? player.Key : null;
        }

        public Player UpdateConnection(string connectionId, string newConnectionId, Player player)
        {
            RemovePlayer(connectionId);
            AddPlayer(player, newConnectionId);
            return GetPlayer(newConnectionId);
        }
    }
}

using System;
using GameService.Domain.Entities.Base;
using GameService.Domain.Enums;

namespace GameService.Domain.Entities;

public class Player : AuditEntity
{
    public string DeviceId { get; set; } = null!;
    public int Coins { get; set; }
    public int Rolls { get; set; }

    public bool BalanceIsAvailableForGit(ResourceType resourceType, int amount)
    {
        return resourceType switch
        {
            ResourceType.Rolls => Rolls >= amount,
            ResourceType.Coins => Coins >= amount,
            _ => false
        };
    }

    public void SendGift(Player receiver, ResourceType resourceType, int amount)
    {
        switch (resourceType)
        {
            case ResourceType.Rolls:
                receiver.Rolls += amount;
                Rolls -= amount;
                break;
            case ResourceType.Coins:
                receiver.Coins += amount;
                Coins -= amount;
                break;
        }
    }
}
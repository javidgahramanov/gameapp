using GameService.Domain.Enums;

namespace GameService.Domain.Models;

public class GiftSentResponse
{
    public ResourceType ResourceType { get; set; }
    public int Amount { get; set; }
    public string DeviceId { get; set; }
}
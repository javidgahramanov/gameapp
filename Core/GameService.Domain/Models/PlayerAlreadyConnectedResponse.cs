using System;

namespace GameService.Domain.Models;

public class PlayerAlreadyConnectedResponse
{
    public string ConnectionId { get; set; }
    public Guid PlayerId { get; set; }
    public string DeviceId { get; set; }
}
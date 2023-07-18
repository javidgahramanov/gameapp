using GameService.Domain.Enums;

namespace GameService.Domain.Models
{
    public class BalanceIsNotAvailableResponse
    {
        public ResourceType ResourceType { get; set; }
        public int Amount { get; set; }
    }
}

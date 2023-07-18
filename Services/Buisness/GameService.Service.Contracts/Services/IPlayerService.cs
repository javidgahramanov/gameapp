using GameService.Domain.Entities;
using GameService.Service.Contracts.Services.Base;

namespace GameService.Service.Contracts.Services
{
    public interface IPlayerService : ICrudService<Player>
    {
    }
}

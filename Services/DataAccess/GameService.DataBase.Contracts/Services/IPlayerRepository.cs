using GameService.DataBase.Contracts.Services.Base;
using GameService.Domain.Entities;

namespace GameService.DataBase.Contracts.Services
{
    public interface IPlayerRepository : ICrudRepository<Player>
    {
    }
}

using GameService.Database.Implementation.Services.Base;
using GameService.DataBase.Contracts.Services;
using GameService.DataContracts.Contracts;
using GameService.Domain.Entities;

namespace GameService.Database.Implementation.Services
{
    public class PlayerRepository : BaseCrudRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
       
    }
}

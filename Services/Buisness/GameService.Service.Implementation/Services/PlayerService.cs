using GameService.DataBase.Contracts.Services;
using GameService.DataContracts.Contracts;
using GameService.Domain.Entities;
using GameService.Service.Contracts.Services;
using GameService.Service.Implementation.Services.Base;

namespace GameService.Service.Implementation.Services
{
    public class PlayerService : BaseCrudService<Player, IPlayerRepository>, IPlayerService
    {
        public PlayerService(IUnitOfWorkFactory unitOfWorkFactory, IPlayerRepository repository) : base(unitOfWorkFactory, repository)
        {
        }
    }
}

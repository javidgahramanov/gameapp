using Microsoft.EntityFrameworkCore;

namespace GameService.DataContracts.Contracts
{
    public interface IDbContextFactory
    {
        DbContext GetContext();
    }
}

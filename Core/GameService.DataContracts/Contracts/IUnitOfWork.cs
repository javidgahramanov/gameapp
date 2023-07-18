using System;
using System.Threading.Tasks;

namespace GameService.DataContracts.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
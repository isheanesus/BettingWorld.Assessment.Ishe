using BettingWorld.Assessment.Ishe.API.Models;

namespace BettingWorld.Assessment.Ishe.API.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<CurrencyRatesHistory> CurrencyRatesHistoryRepository { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

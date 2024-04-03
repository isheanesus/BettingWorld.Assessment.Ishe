using BettingWorld.Assessment.Ishe.API.Data;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using BettingWorld.Assessment.Ishe.API.Models;
using BettingWorld.Assessment.Ishe.API.Repositories;

namespace BettingWorld.Assessment.Ishe.API.Helpers
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RatesContext _context;
        private IRepository<CurrencyRatesHistory> _ratesRepository;
        public UnitOfWork(RatesContext context)
        {
            _context = context;
        }

        public IRepository<CurrencyRatesHistory> CurrencyRatesHistoryRepository
        {
            get
            {
                if (_ratesRepository == null)
                {
                    _ratesRepository = new RatesHistoryRepository(_context);
                }
                return _ratesRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

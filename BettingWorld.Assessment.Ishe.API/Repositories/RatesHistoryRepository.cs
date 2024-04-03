using BettingWorld.Assessment.Ishe.API.Data;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using BettingWorld.Assessment.Ishe.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BettingWorld.Assessment.Ishe.API.Repositories
{
    public class RatesHistoryRepository : IRepository<CurrencyRatesHistory>
    {
        public readonly RatesContext _context;
        public RatesHistoryRepository(RatesContext context) 
        {
            _context = context;
        }
        public async Task CreateAsync(CurrencyRatesHistory entity)
        {
            await _context.CurrencyRatesHistory.AddAsync(entity);
        }

        public async Task<ICollection<CurrencyRatesHistory>> ListAsync()
        {
            return await _context.CurrencyRatesHistory.ToListAsync();
        }
    }
}

using BettingWorld.Assessment.Ishe.API.Dtos;
using BettingWorld.Assessment.Ishe.API.Models;

namespace BettingWorld.Assessment.Ishe.API.Interfaces
{
    public interface IRatesService
    {
        public Task<CurrencyRates> GetRates();

        public Task<decimal> GetRate(string rateName);
    }
}

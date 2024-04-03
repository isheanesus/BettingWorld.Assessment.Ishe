using BettingWorld.Assessment.Ishe.API.Dtos;
using BettingWorld.Assessment.Ishe.API.Models;

namespace BettingWorld.Assessment.Ishe.API.Interfaces
{
    public interface IExternalRatesService
    {
        public Task<CurrencyRates> GetExternalRates();
    }
}

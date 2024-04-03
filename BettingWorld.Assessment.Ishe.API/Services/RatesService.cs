using BettingWorld.Assessment.Ishe.API.Dtos;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using BettingWorld.Assessment.Ishe.API.Models;
using freecurrencyapi;
using freecurrencyapi.Helpers;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;

namespace BettingWorld.Assessment.Ishe.API.Services
{
    public class RatesService : IRatesService
    {
        
        private readonly IExternalRatesService _externalRatesService;
        private readonly IDistributedCache _cache;
        private readonly IUnitOfWork _unitOfWork;

        public RatesService(

            IExternalRatesService externalRatesService, 
            IDistributedCache cache,
            IUnitOfWork unitOfWork)
        {
            
            _externalRatesService = externalRatesService;
            _cache = cache;
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> GetRate(string rateName)
        {
            var rates = await GetRates();

            decimal rate;
            rates.Rates.TryGetValue(rateName.ToUpper(), out rate);

            if (rate == 0M)
            {
                throw new NullReferenceException($"The rate for {rateName} was not found. Please try another currency.");
            }

            return rate;
        }

        public async Task<CurrencyRates> GetRates()
        {
            string key = "currency_rates";
            CurrencyRates? rates;

            try
            {
                string? cachedRates = await _cache.GetStringAsync(key);

                if (string.IsNullOrEmpty(cachedRates))
                {
                    rates = await _externalRatesService.GetExternalRates();

                    // set the time limit on the cache.
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                    await _cache.SetStringAsync(key, JsonConvert.SerializeObject(rates), options);
                    await persistRates(rates);

                    return rates;
                }

                rates = JsonConvert.DeserializeObject<CurrencyRates>(cachedRates);
            }
            catch (Exception ex )
            {
                throw new Exception("An error occured when getting the rates " + ex.Message);
            }
            
            return rates;
        } 

        private async Task persistRates(CurrencyRates rates)
        {
            try
            {
                await _unitOfWork.CurrencyRatesHistoryRepository.CreateAsync(rates.AsCurrencyRateHistory());
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to persist rates to database." + ex.Message);
            }
            
        }
    }
}

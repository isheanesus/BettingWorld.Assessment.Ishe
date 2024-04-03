using BettingWorld.Assessment.Ishe.API.Dtos;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using BettingWorld.Assessment.Ishe.API.Models;
using freecurrencyapi;
using System.Text.Json;

namespace BettingWorld.Assessment.Ishe.API.Services
{
    public class ExternalRateService: IExternalRatesService
    {
        private readonly Freecurrencyapi _fx;
        public ExternalRateService(Freecurrencyapi fx)
        {
            _fx = fx;
        }

        public async Task<CurrencyRates> GetExternalRates()
        {
            var respData = await Task.Run(() => _fx.Latest());

            var jsonData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(respData);
            var ratesDictionary = new Dictionary<string, decimal>();
            jsonData?.TryGetValue("data", out ratesDictionary);

            var ratesDto = new CurrencyRates
            {
                Rates = ratesDictionary
            };

            return ratesDto;
        }
    }
}

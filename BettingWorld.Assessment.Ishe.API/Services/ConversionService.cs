using BettingWorld.Assessment.Ishe.API.Dtos;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using System.Diagnostics;

namespace BettingWorld.Assessment.Ishe.API.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IRatesService _rateService;
        public ConversionService(IRatesService rateService)
        {
            _rateService = rateService;
        }
        public async Task<decimal> Convert(string fromRate, string toRate, decimal amount)
        {
            //  assuming the usd is the base rate that our service gives us you need to 
            //  determine if how to do TagHelperServicesExtensions conversion 
            //  or if you are doing a cross conversion
            decimal result;
            if (fromRate.ToUpper() == "USD" )
            {
                result = await FromUsdConversion(toRate, amount);
            }
            else if(toRate.ToUpper() == "USD")
            {
                result = await ToUsdConversion(fromRate, amount);
            }
            else
            {
                result = await CrossConversion(fromRate, toRate, amount);
            }

            return result;

        }

        private async Task<decimal> CrossConversion(string fromRate, string toRate, decimal amount)
        {

            var converted = await ToUsdConversion(fromRate, amount);
            return await FromUsdConversion(toRate, converted); 

        }

        private async Task<decimal> FromUsdConversion(string toRate, decimal amount)
        {
            decimal usdRate = await _rateService.GetRate(toRate);
            return usdRate * amount;
        }

        private async Task<decimal> ToUsdConversion(string fromRate, decimal amount)
        {
            decimal rate = await _rateService.GetRate(fromRate);
            return amount/rate;
        }


    }
}

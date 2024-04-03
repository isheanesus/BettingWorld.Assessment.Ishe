using BettingWorld.Assessment.Ishe.API.Models;
using Google.Protobuf.WellKnownTypes;
using System.Text.Json;

public static class Extensions
{
    public static CurrencyRatesHistory AsCurrencyRateHistory(this CurrencyRates rate)
    {
        return new CurrencyRatesHistory
        {
            Timestamp = rate.Timestamp,
            Rates = JsonSerializer.Serialize(rate.Rates),
        };
    }

    public static CurrencyRates AsCurrencyRates(this CurrencyRatesHistory rateHistory)
    {
        return new CurrencyRates
        {
            Timestamp = rateHistory.Timestamp,
            Rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(rateHistory.Rates),
        };
    }
}

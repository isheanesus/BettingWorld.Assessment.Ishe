using System.Text.Json.Serialization;

namespace BettingWorld.Assessment.Ishe.API.Models
{
    public record CurrencyRates
    {
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, decimal> Rates{ get; set; }

        public CurrencyRates()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}

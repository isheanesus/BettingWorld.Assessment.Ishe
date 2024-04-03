using BettingWorld.Assessment.Ishe.API.Models;

namespace BettingWorld.Assessment.Ishe.API.Dtos
{
    public record RatesDto
    {
        public DateTime TimeStamp { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }

        public RatesDto()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }
}



namespace BettingWorld.Assessment.Ishe.API.Interfaces;

public interface IConversionService
{ 
    public Task<decimal> Convert(string fromRate, string toRate, decimal amount);
}


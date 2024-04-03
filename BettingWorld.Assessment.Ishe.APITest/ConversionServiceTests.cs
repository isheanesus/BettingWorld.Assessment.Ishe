using NUnit.Framework;
using BettingWorld.Assessment.Ishe.API.Services;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using Moq;
using System.Threading.Tasks;

namespace BettingWorld.Assessment.Ishe.Tests
{
    [TestFixture]
    public class ConversionServiceTests
    {
        private Mock<IRatesService> _mockRatesService;
        private ConversionService _conversionService;

        [SetUp]
        public void Setup()
        {
            _mockRatesService = new Mock<IRatesService>();
            _conversionService = new ConversionService(_mockRatesService.Object);
        }

        [Test]
        public async Task Convert_FromUSDToOtherCurrency_ReturnsConvertedAmount()
        {

            string fromRate = "USD";
            string toRate = "EUR";
            decimal amount = 100;
            decimal expectedRate = 0.85m;
            decimal expectedAmount = amount * expectedRate;
            _mockRatesService.Setup(service => service.GetRate(toRate)).ReturnsAsync(expectedRate);

            // do the actual conversion
            decimal result = await _conversionService.Convert(fromRate, toRate, amount);

            // Assert
            Assert.AreEqual(expectedAmount, result);
        }

        [Test]
        public async Task Convert_FromOtherCurrencyToUSD_ReturnsConvertedAmount()
        {
            
            string fromRate = "EUR";
            string toRate = "USD";
            decimal amount = 100;
            decimal expectedRate = 1.18m;
            decimal expectedAmount = amount / expectedRate;
            _mockRatesService.Setup(service => service.GetRate(fromRate)).ReturnsAsync(expectedRate);

            // do the actual conversion
            decimal result = await _conversionService.Convert(fromRate, toRate, amount);

            // Assert
            Assert.AreEqual(expectedAmount, result);
        }

        [Test]
        public async Task Convert_CrossConversion_ReturnsConvertedAmount()
        {
            // Arrange
            string fromRate = "GBP";
            string toRate = "EUR";
            decimal amount = 100;
            decimal rateToUSD = 1.38m;
            decimal rateFromUSD = 0.85m;
            decimal expectedAmount = (amount / rateToUSD) * rateFromUSD;
            _mockRatesService.Setup(service => service.GetRate(fromRate)).ReturnsAsync(rateToUSD);
            _mockRatesService.Setup(service => service.GetRate(toRate)).ReturnsAsync(rateFromUSD);

            // do the actual x conversion
            decimal result = await _conversionService.Convert(fromRate, toRate, amount);

            // Assert
            Assert.AreEqual(expectedAmount, result);
        }
    }
}

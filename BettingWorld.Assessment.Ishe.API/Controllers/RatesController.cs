using BettingWorld.Assessment.Ishe.API.Dtos;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using BettingWorld.Assessment.Ishe.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BettingWorld.Assessment.Ishe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRatesService _ratesService;
        private readonly IUnitOfWork _unitOfWork;
        public RatesController(IRatesService ratesService, IUnitOfWork unitOfWork)
        {
            _ratesService = ratesService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("history")]
        public async Task<ActionResult<ICollection<CurrencyRatesHistory>>> Get()
        {
            try
            {
                var ratesHistory = await _unitOfWork.CurrencyRatesHistoryRepository.ListAsync();
                return ratesHistory.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured." + ex.Message);
            }
            
        }
    }
}

using BettingWorld.Assessment.Ishe.API.Dtos;
using BettingWorld.Assessment.Ishe.API.Interfaces;
using BettingWorld.Assessment.Ishe.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BettingWorld.Assessment.Ishe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly IConversionService _conversionService;
        public ConvertController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }
        // GET: api/<ConvertController>
        [HttpGet("getConversion/{fromRate}/{toRate}/{amount}")]
        public async Task<ActionResult<decimal>> ConvertRate(string fromRate, string toRate, decimal amount)
        {
            try
            {
               var converted = await _conversionService.Convert(fromRate, toRate, amount);
                return converted;
            }
            catch( NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured while processing request. {ex.Message}");
            }
        }

    }
}

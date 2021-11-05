using System;
using StocksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StocksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ILogger<StocksController> _logger;

        public StocksController(ILogger<StocksController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{symbol}")]
        public IActionResult Get(string symbol)
        {
            try
            {
                
                // if the symbol is Microsoft, return the Ok with data (200)
                if (symbol == "MSFT")
                    return Ok(
                         new Stock(){
                        Symbol = "MSFT",
                        Name = "Microsoft",
                        Price = 100.01,
                        Date = new DateTime()
                    }
                    );

                // if not Microsoft, return Ok NoContent (204)
                return NoContent();
            }
            catch(Exception ex)
            {
                // log exceptions
                _logger.LogError(ex.Message, ex);

                // return 
                return StatusCode(500);
            }

        }
    }
}

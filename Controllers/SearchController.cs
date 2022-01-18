using System;
using StocksApi.Models;
using StocksApi.Interfaces;
using StocksApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SearchApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IStocksManager _stocksManager;

        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger, IStocksManager stocksManager)
        {
            _logger = logger;
            _stocksManager = stocksManager;
        }

        [HttpGet("{symbol}")]
        public IActionResult Get(string symbol)
        {
            try
            {
                Stock stock = _stocksManager.GetStock(symbol);

                return stock != null ? Ok(stock) : NoContent();
            }
            catch (StocksApi.Exceptions.YahooException ex)
            {
                // log exception
                _logger.LogError(ex.Message, ex);

                // return
                return StatusCode(500, new {StatusCode=500, Title=ex.Message });
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

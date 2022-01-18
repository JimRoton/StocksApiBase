﻿using System;
using StocksApi.Models;
using StocksApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StocksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IStocksManager _stocksManager;

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
            catch (Exceptions.YahooException ex)
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
            // try
            // {
                
            //     // if the symbol is Microsoft, return the Ok with data (200)
            //     if (symbol == "MSFT")
            //         return Ok(
            //              new Stock(){
            //             Symbol = "MSFT",
            //             Name = "Microsoft",
            //             Price = 100.01,
            //             Date = new DateTime()
            //         }
            //         );

            //     // if not Microsoft, return Ok NoContent (204)
            //     return NoContent();
            // }
            // catch(Exception ex)
            // {
            //     // log exceptions
            //     _logger.LogError(ex.Message, ex);

            //     // return 
            //     return StatusCode(500);
            // }

        }
    }
}

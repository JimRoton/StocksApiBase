using StocksApi.Models;
using StocksApi.Interfaces;
using Microsoft.Extensions.Logging;

namespace StocksApi
{
    public class StocksManager : IStocksManager
    {
        private readonly ILogger<StocksManager> _logger;
        private readonly IRedisService _redisService;
        private readonly IYahooService _yahooService;

        public StocksManager(ILogger<StocksManager> logger, IRedisService redisService, IYahooService yahooService)
        {
            _logger = logger;
            _redisService = redisService;
            _yahooService = yahooService;
        }
        public Stock GetStock(string symbol)
        {
            // check redis
            if (_redisService.HasStock(symbol))
                return _redisService.GetStock(symbol);
            
            // get stock from yahoo
            Stock stock = _yahooService.GetStock(symbol);

            // add to redis
            _redisService.AddStock(stock);

            return stock;
        }
    }
}
using System;
using System.Text.Json;
using SearchApi.Configs;
using StocksApi.Models;
using StackExchange.Redis;
using StocksApi.Interfaces;
using Microsoft.Extensions.Options;

namespace StocksApi.Services
{


    public class RedisService : IRedisService
    {
        private ConnectionMultiplexer redis;

        private int ttl;

        public RedisService(IOptions<RedisConfig> config)
        {
            // check for connection string
            if (config.Value.connectionString == null)
                throw new ArgumentNullException("connectionString");

            // set ttl for data
            ttl = config.Value.ttl > 0 ? config.Value.ttl : 90;

            // create connection to redis
            redis = ConnectionMultiplexer.Connect(
                new ConfigurationOptions{
                    EndPoints = {
                        config.Value.connectionString
                    }
                }
            );
        }

        public Stock GetStock(string symbol)
        {
            var db = redis.GetDatabase();
            
            // get the stock from cache
            Stock stock = JsonSerializer.Deserialize<Stock>(
                db.StringGet(symbol)
            );

            // update source
            stock.Source = "REDIS";

            return stock;
        }

        public bool HasStock(string symbol)
        {
            IDatabase db = redis.GetDatabase();
            
            return db.KeyExists(symbol);
        }

        public void AddStock(Stock stock)
        {
            IDatabase db = redis.GetDatabase();

            // add stock to redis cache
            db.StringSet(stock.Symbol, JsonSerializer.Serialize(stock), new TimeSpan(0, 0, ttl));
        }
    }
}
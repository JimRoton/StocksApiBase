using StocksApi.Models;

namespace StocksApi.Interfaces
{
    public interface IRedisService : IStockService
    {
        bool HasStock(string symbol);

        void AddStock(Stock stock);
    }
}
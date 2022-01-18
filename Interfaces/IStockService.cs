using StocksApi.Models;

namespace StocksApi.Interfaces
{
    public interface IStockService
    {
        Stock GetStock(string symbol);
    }
}
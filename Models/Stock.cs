using System;

namespace StocksApi.Models
{
    public class Stock
    {
        public DateTime Date { get; set; }

        public string Symbol { get; set; }

        public string Name  { get; set; }

        public double Price { get; set; }

        public string Source { get; set; }
    }
}

using System;

namespace StocksApi.Exceptions
{
    public class YahooException : Exception
    {
        public YahooException(string exception) :
            base($"Yahoo Exception: {exception}")
            {

            }
    }
}
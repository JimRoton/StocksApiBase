using System;
using System.Net;
using System.Text.Json;
using StocksApi.Models;
using StocksApi.Interfaces;
using Microsoft.Extensions.Options;
using SearchApi.Configs;

namespace StocksApi.Services
{
    public class YahooService : IYahooService
    {
        private readonly string serviceUrl;

        public YahooService(IOptions<YahooConfig> config)
        {
            if (config.Value.serviceUrl == null)
                throw new ArgumentNullException("serviceUrl");
                
            serviceUrl = config.Value.serviceUrl;
        }

        public Stock GetStock(string symbol)
        {
            string data;

            using (var wc = new WebClient())
            {
                data = wc.DownloadString(string.Format(serviceUrl, symbol));
            }
            
            if (data == null)
                return null;
            
            YahooResponse response = JsonSerializer.Deserialize<YahooResponse>(data);

            // make sure we received something
            if (response == null)
                return null;
            else if (response.quoteResponse == null)
                return null;

            /***********************************
            * if we check for errors first and throw them,
            *  we could be giving up data due to an error
            *  or warning
            * if we check for stocks first, we could be masking
            *  errors.
            * we could:
            *  - throw errors
            *  - log errors but return
            *  - return only
            * which should we do first?
            ************************************/
            
            // if data is found, go ahead and return it
            if (response.quoteResponse.HasResponses())
                return new Stock(){
                    Name = response.quoteResponse.result[0].shortName,
                    Symbol = response.quoteResponse.result[0].symbol,
                    Price = response.quoteResponse.result[0].regularMarketPrice,
                    Date = DateTime.Now,
                    Source = "YAHOO"
                };
            
            // if no data check for error and throw it
            if (!string.IsNullOrWhiteSpace(response.quoteResponse.error))
                throw new StocksApi.Exceptions.YahooException(response.quoteResponse.error);
            
            // no data or error
            return null;                
        }
    }
}
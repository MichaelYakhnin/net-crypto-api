using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices
{
    public class BitfinexPublicApi : RestApi
    {
        private const string ticker = "https://api.bitfinex.com/v1/pubticker/";
        private const string orderBook = "https://api.bitfinex.com/v1/book/";

        public JObject GetTicker(string asset)
        {
            return Send(ticker + asset);
        }

        public JObject GetOrderBook(string asset,string limit_bids = "25",string limit_asks = "25",string group = "1")
        {
            return Send(orderBook + asset + "?limit_bids=" + limit_bids + "&limit_asks=" + limit_asks + "&group=" + group);
        }

    }
}

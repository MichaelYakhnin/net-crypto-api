using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TickerMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TickerMonitor
{
    public class BitstampPublicApi : RestApi, IPublicApi
    {
        private const string TICKER = "https://www.bitstamp.net/api/v2/ticker/";
        
        private BitstampOrderBook orderBookBitstamp;
        private BitstampTicker bitstampTicker;
        private string assetOrder;

        public LastDataOrderBook lastDataOrderBook { get; set; }
        public string url { get; set; }
        public int Order_num { get ; set; }

        public void GetTicker(string asset)
        {
            string json = Send(TICKER + asset + "/");
            if (json != null) bitstampTicker = Loader.Load<BitstampTicker>(json);
        }

        public void GetOrderBook(string asset)
        {
            assetOrder = asset;
            string json = Send(url + asset + "/");
            if (json != null) orderBookBitstamp = Loader.Load<BitstampOrderBook>(json);
        }
        public void GetNdataFromOrderBook()
        {
            lastDataOrderBook = new LastDataOrderBook();
            lastDataOrderBook.Provider = "Bitstamp";
            lastDataOrderBook.AssetName = assetOrder;
            lastDataOrderBook.asks.price = orderBookBitstamp.asks[Order_num - 1][0];
            lastDataOrderBook.asks.amount = orderBookBitstamp.asks[Order_num - 1][1];
            lastDataOrderBook.asks.timestamp = orderBookBitstamp.timestamp;
            lastDataOrderBook.bids.price = orderBookBitstamp.bids[Order_num - 1][0];
            lastDataOrderBook.bids.amount = orderBookBitstamp.bids[Order_num - 1][1];
            lastDataOrderBook.bids.timestamp = orderBookBitstamp.timestamp;

            
        }

        
    }
}

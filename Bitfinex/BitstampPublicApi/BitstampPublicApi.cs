using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExternalServices
{
    public class BitstampPublicApi : RestApi, IPublicApi
    {
        private const string TICKER = "https://www.bitstamp.net/api/v2/ticker/";
        private const string ORDER_BOOK = "https://www.bitstamp.net/api/v2/order_book/";
        private const int COUNT_ROW = 24;
        private BitstampOrderBook orderBookBitstamp;
        private BitstampTicker bitstampTicker;
        private string assetOrder;

        public void GetTicker(string asset)
        {
            string json = Send(TICKER + asset + "/");
            bitstampTicker = Loader.Load<BitstampTicker>(json);
        }

        public void GetOrderBook(string asset)
        {
            assetOrder = asset;
            string json = Send(ORDER_BOOK + asset + "/");
            orderBookBitstamp = Loader.Load<BitstampOrderBook>(json);
        }
        public LastDataOrderBook Get25dataFromOrderBook()
        {
            LastDataOrderBook lastDataOrderBook = new LastDataOrderBook();
            lastDataOrderBook.Provider = "Bitstamp";
            lastDataOrderBook.AssetName = assetOrder;
            lastDataOrderBook.asks.price = orderBookBitstamp.asks[COUNT_ROW][0];
            lastDataOrderBook.asks.amount = orderBookBitstamp.asks[COUNT_ROW][1];
            lastDataOrderBook.asks.timestamp = orderBookBitstamp.timestamp;
            lastDataOrderBook.bids.price = orderBookBitstamp.bids[COUNT_ROW][0];
            lastDataOrderBook.bids.amount = orderBookBitstamp.bids[COUNT_ROW][1];
            lastDataOrderBook.bids.timestamp = orderBookBitstamp.timestamp;

            return lastDataOrderBook;
        }
    }
}

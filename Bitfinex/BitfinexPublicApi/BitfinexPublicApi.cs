using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices
{
    public class BitfinexPublicApi : RestApi, IPublicApi
    {
        private const string TICKER = "https://api.bitfinex.com/v1/pubticker/";
        private const string ORDER_BOOK = "https://api.bitfinex.com/v1/book/";
        private const string LIMIT_BIDS = "25";
        private const string LIMIT_ASKS = "25";
        private const string GROUP = "1";
        private const int COUNT_ROW = 24;
        private BitfinexOrderBook _orderBook;
        private BitfinexTicker bitfinexTicker;
        private string assetOrder;

        public void GetTicker(string asset)
        {
            string json = Send(TICKER + asset);
            bitfinexTicker = Loader.Load<BitfinexTicker>(json);
        }

        public void GetOrderBook(string asset)
        {
            assetOrder = asset;
            string json = Send(ORDER_BOOK + asset + "?limit_bids=" + LIMIT_BIDS + "&limit_asks=" + LIMIT_ASKS + "&group=" + GROUP);
             _orderBook = Loader.Load<BitfinexOrderBook>(json);
        }

        public LastDataOrderBook Get25dataFromOrderBook()
        {
            LastDataOrderBook lastDataOrderBook = new LastDataOrderBook();
            lastDataOrderBook.Provider = "Bitfinex";
            lastDataOrderBook.AssetName = assetOrder;
            lastDataOrderBook.asks.price = _orderBook.asks[COUNT_ROW].price;
            lastDataOrderBook.asks.amount = _orderBook.asks[COUNT_ROW].amount;
            lastDataOrderBook.asks.timestamp = _orderBook.asks[COUNT_ROW].timestamp;
            lastDataOrderBook.bids.price = _orderBook.bids[COUNT_ROW].price;
            lastDataOrderBook.bids.amount = _orderBook.bids[COUNT_ROW].amount;
            lastDataOrderBook.bids.timestamp = _orderBook.bids[COUNT_ROW].timestamp;

            return lastDataOrderBook;
        }

    }
   
}

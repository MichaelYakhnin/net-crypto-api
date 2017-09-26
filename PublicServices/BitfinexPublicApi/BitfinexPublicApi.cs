using Newtonsoft.Json.Linq;
using TickerMonitor;
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using System.Threading.Tasks;

namespace TickerMonitor
{
    public class BitfinexPublicApi : RestApi, IPublicApi
    {
        public  string TICKER = "https://api.bitfinex.com/v1/pubticker/";
        public string  url  { get; set; }

        private const string GROUP = "1";
        public int Order_num { get; set; }
       
        private BitfinexOrderBook _orderBook;
        private BitfinexTicker bitfinexTicker;
        public LastDataOrderBook lastDataOrderBook { get; set; }
        private string assetOrder;
        
        public void GetTicker(string asset)
        {
            assetOrder = asset;
            string json = Send(TICKER + asset);
            if(json != null)
               bitfinexTicker = Loader.Load<BitfinexTicker>(json);
        }

        public void GetOrderBook(string asset)
        {
            assetOrder = asset;
            string json = Send(url + asset + "?limit_bids=" + Order_num + "&limit_asks=" + Order_num + "&group=" + GROUP);
            if (json != null)
                _orderBook = Loader.Load<BitfinexOrderBook>(json);
        }

        public void GetNdataFromOrderBook()
        {
            if(_orderBook != null)
            {
                lastDataOrderBook = new LastDataOrderBook();
                lastDataOrderBook.Provider = "Bitfinex";
                lastDataOrderBook.AssetName = assetOrder;
                lastDataOrderBook.asks.price = _orderBook.asks[Order_num-1].price;
                lastDataOrderBook.asks.amount = _orderBook.asks[Order_num - 1].amount;
                lastDataOrderBook.asks.timestamp = _orderBook.asks[Order_num - 1].timestamp;
                lastDataOrderBook.bids.price = _orderBook.bids[Order_num - 1].price;
                lastDataOrderBook.bids.amount = _orderBook.bids[Order_num - 1].amount;
                lastDataOrderBook.bids.timestamp = _orderBook.bids[Order_num - 1].timestamp;
            }
           
            
        }



        
    }
   
}

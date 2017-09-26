using System;
using Quartz;
using System.Threading.Tasks;// included along with Quartz

namespace TickerMonitor
{
    public class OrdersService : IJob
    {
        BitfinexPublicApi bitfinexPublicApi;
        BitstampPublicApi bitstampPublicApi;
        LastDataOrderBook lastDataOrderBook;

        public OrdersService()
        {
           // bitfinexPublicApi = new BitfinexPublicApi();
            bitstampPublicApi = new BitstampPublicApi();
            lastDataOrderBook = new LastDataOrderBook();
            //Console.Write(bit.GetTicker("BTCUSD"));
            
        }
        public async Task Execute(IJobExecutionContext context)
        {
            GetOrderBookByTicker(bitfinexPublicApi, "BTCUSD");
            GetOrderBookByTicker(bitstampPublicApi, "btcusd");
            GetOrderBookByTicker(bitfinexPublicApi, "ETHUSD");
            GetOrderBookByTicker(bitstampPublicApi, "ethusd");
            GetOrderBookByTicker(bitfinexPublicApi, "LTCUSD");
            GetOrderBookByTicker(bitstampPublicApi, "ltcusd");
            await Console.Out.WriteLineAsync("Last orders!");
        }
        private void GetOrderBookByTicker(IPublicApi publicApi, string ticker)
        {
            publicApi.GetOrderBook(ticker);
            publicApi.GetNdataFromOrderBook();
            publicApi.GetOrderBook(ticker);
            publicApi.GetNdataFromOrderBook();
            Console.WriteLine("Provider:" + publicApi.lastDataOrderBook.Provider + " " + publicApi.lastDataOrderBook.AssetName + " Bid:" + publicApi.lastDataOrderBook.bids.price + " Amount:" + publicApi.lastDataOrderBook.bids.amount + " Timestamp:" + Utils.UnixTimeStampToDateTime(double.Parse(publicApi.lastDataOrderBook.bids.timestamp)));
            Console.WriteLine("Provider:" + publicApi.lastDataOrderBook.Provider + " " + publicApi.lastDataOrderBook.AssetName + " Ask:" + publicApi.lastDataOrderBook.asks.price + " Amount:" + publicApi.lastDataOrderBook.asks.amount + " Timestamp:" + Utils.UnixTimeStampToDateTime(double.Parse(publicApi.lastDataOrderBook.bids.timestamp)));
        }

    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using System.Threading.Tasks;// included along with Quartz
using Quartz.Impl;

namespace ExternalServices
{
    public class OrdersService : IJob
    {
        BitfinexPublicApi bitfinexPublicApi;
        BitstampPublicApi bitstampPublicApi;
        LastDataOrderBook lastDataOrderBook;

        public OrdersService()
        {
            bitfinexPublicApi = new BitfinexPublicApi();
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
            lastDataOrderBook = publicApi.Get25dataFromOrderBook();
            Console.WriteLine("Provider:" + lastDataOrderBook.Provider + " " + lastDataOrderBook.AssetName + " Bid:" + lastDataOrderBook.bids.price + " Amount:" + lastDataOrderBook.bids.amount + " Timestamp:" + Utils.UnixTimeStampToDateTime(double.Parse(lastDataOrderBook.bids.timestamp)));
            Console.WriteLine("Provider:" + lastDataOrderBook.Provider + " " + lastDataOrderBook.AssetName + " Ask:" + lastDataOrderBook.asks.price + " Amount:" + lastDataOrderBook.asks.amount + " Timestamp:" + Utils.UnixTimeStampToDateTime(double.Parse(lastDataOrderBook.bids.timestamp)));
        }

    }
}
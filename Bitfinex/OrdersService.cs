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
        BitfinexPublicApi bit;
        BitstampPublicApi bitstampPublicApi;
        LastDataOrderBook lastDataOrderBook;

        public OrdersService()
        {
            bit = new BitfinexPublicApi();
            bitstampPublicApi = new BitstampPublicApi();
            lastDataOrderBook = new LastDataOrderBook();
            //Console.Write(bit.GetTicker("BTCUSD"));
            
        }
        public async Task Execute(IJobExecutionContext context)
        {
            bit.GetOrderBook("BTCUSD");
            bitstampPublicApi.GetOrderBook("btcusd");

            lastDataOrderBook = bit.Get25dataFromOrderBook();
            Console.WriteLine("Provider:"+ lastDataOrderBook.Provider + " "+ lastDataOrderBook.AssetName + " Bid:" + lastDataOrderBook.bids.price + " Amount:" + lastDataOrderBook.bids.amount);
            
            lastDataOrderBook = bitstampPublicApi.Get25dataFromOrderBook();
            Console.WriteLine("Provider:" + lastDataOrderBook.Provider + " " + lastDataOrderBook.AssetName + " Bid:" + lastDataOrderBook.bids.price + " Amount:" + lastDataOrderBook.bids.amount);
            await Console.Out.WriteLineAsync("Last orders!");
        }

    }
}
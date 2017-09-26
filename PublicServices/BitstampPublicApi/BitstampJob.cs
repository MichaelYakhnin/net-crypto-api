using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TickerMonitor
{
    public class BitstampJob :IJob
    {
        private BitstampPublicApi bitstampPublicApi;

        public BitstampJob()
        {
            bitstampPublicApi = new BitstampPublicApi(); ;
        }

        public Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            bitstampPublicApi.url = dataMap.GetString("url");
            bitstampPublicApi.Order_num = dataMap.GetInt("OrderNumber");
            GetOrderBookByTicker(bitstampPublicApi, context.JobDetail.Key.Name);
            return Task.CompletedTask;
        }

        private void GetOrderBookByTicker(IPublicApi publicApi, string ticker)
        {
            publicApi.GetOrderBook(ticker);
            publicApi.GetNdataFromOrderBook();
            Console.WriteLine("Provider:" + publicApi.lastDataOrderBook.Provider + " " + publicApi.lastDataOrderBook.AssetName + " Bid:" + publicApi.lastDataOrderBook.bids.price + " Amount:" + publicApi.lastDataOrderBook.bids.amount + " Timestamp:" + Utils.UnixTimeStampToDateTime(double.Parse(publicApi.lastDataOrderBook.bids.timestamp)));
            Console.WriteLine("Provider:" + publicApi.lastDataOrderBook.Provider + " " + publicApi.lastDataOrderBook.AssetName + " Ask:" + publicApi.lastDataOrderBook.asks.price + " Amount:" + publicApi.lastDataOrderBook.asks.amount + " Timestamp:" + Utils.UnixTimeStampToDateTime(double.Parse(publicApi.lastDataOrderBook.bids.timestamp)));
        }
    }
}

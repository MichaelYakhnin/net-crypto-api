using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TickerMonitor
{
    public class RateJobListener : IJobListener
    {
        private int counter = 1;
        public string Name => "RateJobListener" + Interlocked.Increment(ref counter).ToString();

        public delegate void OrderBookHandler(LastDataOrderBook lastDataOrderBook);
        
        public static event OrderBookHandler orderbookhandler;

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
           // Console.WriteLine("JobExecutionVetoed");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Console.WriteLine("JobToBeExecuted");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                dynamic resultObject = context.JobInstance;
                if (resultObject != null)
                {
                    Console.WriteLine("Provider:" + resultObject.publicApi.lastDataOrderBook.Provider + " " + resultObject.publicApi.lastDataOrderBook.AssetName + " Bid:" + resultObject.publicApi.lastDataOrderBook.bids.price + " Amount:" + resultObject.publicApi.lastDataOrderBook.bids.amount + " Timestamp:" + Utils.UnixTimeStampToDateTime(double.Parse(resultObject.publicApi.lastDataOrderBook.bids.timestamp)));
                    Console.WriteLine("Provider:" + resultObject.publicApi.lastDataOrderBook.Provider + " " + resultObject.publicApi.lastDataOrderBook.AssetName + " Ask:" + resultObject.publicApi.lastDataOrderBook.asks.price + " Amount:" + resultObject.publicApi.lastDataOrderBook.asks.amount + " Timestamp:" + Utils.UnixTimeStampToDateTime(double.Parse(resultObject.publicApi.lastDataOrderBook.bids.timestamp)));
                    Console.WriteLine("JobWasExecuted");
                    orderbookhandler?.Invoke(resultObject.publicApi.lastDataOrderBook);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exeption Occured: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}

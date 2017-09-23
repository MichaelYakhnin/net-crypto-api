
using ExternalServices;
using Jojatekok.BitstampAPI;
using Jojatekok.BitstampAPI.MarketTools;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBitfinex
{
    class Program
    {
        

        static void Main(string[] args)
        {

            //BlockchainInfo.Connect("wss://ws.blockchain.info/inv").Wait();
            
            
            BitfinexPublicApi bit = new BitfinexPublicApi();
            //Console.Write(bit.GetTicker("BTCUSD"));
            bit.GetOrderBook("BTCUSD");

            LastDataOrderBook lastDataOrderBook = new LastDataOrderBook();
            lastDataOrderBook = bit.Get25dataFromOrderBook();
            Console.WriteLine(lastDataOrderBook.AssetName + " Bid:"+ lastDataOrderBook.bids.price+" Amount:"+ lastDataOrderBook.bids.amount);

            BitstampPublicApi bitstampPublicApi = new BitstampPublicApi();
            bitstampPublicApi.GetOrderBook("btcusd");

            lastDataOrderBook = bitstampPublicApi.Get25dataFromOrderBook();
            Console.WriteLine(lastDataOrderBook.AssetName + " Bid:" + lastDataOrderBook.bids.price + " Amount:" + lastDataOrderBook.bids.amount);

            //quartz
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
            
            
                RunProgramRunExample().GetAwaiter().GetResult();

            while (true)
            {

            }




                //websocket bitfinex
            BitstampOrderSend bitstampOrderSend = new BitstampOrderSend();
            bitstampOrderSend.Event = "subscribe";
            bitstampOrderSend.Channel = "book";
            bitstampOrderSend.Pair = "BTCUSD";
            bitstampOrderSend.Prec = "P0";
            bitstampOrderSend.Freq = "F0";
           // BitfinexOrdersrWS bitfinexOrdersrWS = new BitfinexOrdersrWS(bitstampOrderSend);

            //Program p = new Program();
            //p.MainWindow();


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        private static async Task RunProgramRunExample()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<OrdersService>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(5)
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);
                RateJobListener rateJobListener = new RateJobListener();
                scheduler.ListenerManager.AddJobListener(rateJobListener, KeyMatcher<JobKey>.KeyEquals(new JobKey("job1", "group1")));
                // some sleep to show what's happening
                //await Task.Delay(TimeSpan.FromSeconds(60));

                //// and last shut down the scheduler when you are ready to close your program
                //await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }

        // simple log provider to get something to the console
        private class ConsoleLogProvider : ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (level >= LogLevel.Info && func != null)
                    {
                        Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                    }
                    return true;
                };
            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                throw new NotImplementedException();
            }
        }
        private BitstampClient BitstampClient { get; set; }

        private void MainWindow()
        {
            
            BitstampClient = new BitstampClient();
            LoadMarketSummaryAsync();
        }

        private async void LoadMarketSummaryAsync()
        {
            var market = await BitstampClient.Market.GetSummaryAsync();
            string price = "$" + market.PriceLast.ToStringNormalized();
            string buy = market.OrderTopBuy.ToStringNormalized();
            string sell = market.OrderTopSell.ToStringNormalized();
            var market1 = await BitstampClient.Market.GetOpenOrdersAsync();
            IList <IOrder > listBuy = market1.BuyOrders;
            IList <IOrder> listSell = market1.SellOrders;
            Console.WriteLine(price +" "+ buy+" "+sell);
            int i = 0;
            foreach(var l in listBuy)
            {
                i++;
                if (i == 25) break;
                Console.WriteLine("Buy:"+l.AmountBase.ToStringNormalized()+" " +l.AmountQuote.ToStringNormalized()+" "+l.PricePerCoin.ToStringNormalized());
            }
            i = 0;
            foreach (var l in listSell)
            {
                i++;
                if (i == 25) break;
                Console.WriteLine("Sell:" + l.AmountBase.ToStringNormalized() + " " + l.AmountQuote.ToStringNormalized() + " " + l.PricePerCoin.ToStringNormalized());
            }

        }

    }
}


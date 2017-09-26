
using TickerMonitor;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ConsoleBitfinex
{
    class Program
    {
        

        static void Main(string[] args)
        {

            //BlockchainInfo.Connect("wss://ws.blockchain.info/inv").Wait();
            
            //quartz
            LogProvider.SetCurrentLogProvider(new Scheduler.ConsoleLogProvider());

            JobConfig jobConfig1 = new JobConfig
            {
                Provider = "Bitfinex",
                Asset = "BTCUSD",
                OrderNumber =25,
                TimeJobTimer = 3,
                Url = "https://api.bitfinex.com/v1/book/"
            };
            JobConfig jobConfig2 = new JobConfig
            {
                Provider = "Bitfinex",
                Asset = "ETHUSD",
                OrderNumber = 25,
                TimeJobTimer = 4,
                Url = "https://api.bitfinex.com/v1/book/"
            };
            JobConfig jobConfig3 = new JobConfig
            {
                Provider = "Bitstamp",
                Asset = "btcusd",
                OrderNumber = 25,
                TimeJobTimer = 5,
                Url = "https://www.bitstamp.net/api/v2/order_book/"
            };
            JobConfig jobConfig4 = new JobConfig
            {
                Provider = "Bitstamp",
                Asset = "ltcusd",
                OrderNumber = 25,
                TimeJobTimer = 6,
                Url = "https://www.bitstamp.net/api/v2/order_book/"
            };
            JobConfig jobConfig5 = new JobConfig
            {
                Provider = "Bitstamp",
                Asset = "ethusd",
                OrderNumber = 25,
                TimeJobTimer = 2,
                Url = "https://www.bitstamp.net/api/v2/order_book/"
            };
            JobConfig jobConfig6 = new JobConfig
            {
                Provider = "Bitstamp",
                Asset = "ethusd",
                OrderNumber = 25,
                TimeJobTimer = 1,
                Url = "https://www.bitstamp.net/api/v2/order_book/"
            };
            List<JobConfig> job_list = new List<JobConfig>
            {
                jobConfig1,
                jobConfig2,
                jobConfig3,
                jobConfig4,
                jobConfig5,
                jobConfig6
            };
            Scheduler.LoadJobsFromDB(job_list);
            Scheduler.RunJobs().GetAwaiter().GetResult();
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        

        //private BitstampClient BitstampClient { get; set; }

        //private void MainWindow()
        //{
        //    //websocket bitfinex
        //    //BitstampOrderSend bitstampOrderSend = new BitstampOrderSend();
        //    //bitstampOrderSend.Event = "subscribe";
        //    //bitstampOrderSend.Channel = "book";
        //    //bitstampOrderSend.Pair = "BTCUSD";
        //    //bitstampOrderSend.Prec = "P0";
        //    //bitstampOrderSend.Freq = "F0";
        //    // BitfinexOrdersrWS bitfinexOrdersrWS = new BitfinexOrdersrWS(bitstampOrderSend);

        //    //Program p = new Program();
        //    //p.MainWindow();
        //    BitstampClient = new BitstampClient();
        //    LoadMarketSummaryAsync();
        //}

        //private async void LoadMarketSummaryAsync()
        //{
        //    var market = await BitstampClient.Market.GetSummaryAsync();
        //    string price = "$" + market.PriceLast.ToStringNormalized();
        //    string buy = market.OrderTopBuy.ToStringNormalized();
        //    string sell = market.OrderTopSell.ToStringNormalized();
        //    var market1 = await BitstampClient.Market.GetOpenOrdersAsync();
        //    IList <IOrder > listBuy = market1.BuyOrders;
        //    IList <IOrder> listSell = market1.SellOrders;
        //    Console.WriteLine(price +" "+ buy+" "+sell);
        //    int i = 0;
        //    foreach(var l in listBuy)
        //    {
        //        i++;
        //        if (i == 25) break;
        //        Console.WriteLine("Buy:"+l.AmountBase.ToStringNormalized()+" " +l.AmountQuote.ToStringNormalized()+" "+l.PricePerCoin.ToStringNormalized());
        //    }
        //    i = 0;
        //    foreach (var l in listSell)
        //    {
        //        i++;
        //        if (i == 25) break;
        //        Console.WriteLine("Sell:" + l.AmountBase.ToStringNormalized() + " " + l.AmountQuote.ToStringNormalized() + " " + l.PricePerCoin.ToStringNormalized());
        //    }

        //}

    }
}



using ExternalServices;
using Jojatekok.BitstampAPI;
using Jojatekok.BitstampAPI.MarketTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleBitfinex
{
    class Program
    {
        

        static void Main(string[] args)
        {

            //BlockchainInfo.Connect("wss://ws.blockchain.info/inv").Wait();
            //BitfinexTicker bf = new BitfinexTicker(new Asset("subscribe", "ticker", "BTCUSD"));
            // BitfinexOrderBooks bo = new BitfinexOrderBooks(new OrderBooks("subscribe", "book", "BTCUSD", "P0", "F0","25"));
            BitfinexPublicApi bit = new BitfinexPublicApi();
            Console.Write(bit.GetTicker("BTCUSD"));
            //   JObject parsed = bit.GetOrderBook("BTCUSD");
            //   foreach (var pair in parsed)
            //   {
            //       Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            //   }
            Program p = new Program();
            p.MainWindow();


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
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
            IList < IOrder > list = market1.BuyOrders;
            Console.WriteLine(price +" "+ buy+" "+sell);
            foreach(var l in list){
                Console.WriteLine(l.AmountBase.ToStringNormalized()+" " +l.AmountQuote.ToStringNormalized()+" "+l.PricePerCoin.ToStringNormalized());
            }
            
        }

    }
}



using ExternalServices;
using Jojatekok.BitstampAPI;
using Jojatekok.BitstampAPI.MarketTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

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
            //
            OrdersService ordersService = new OrdersService();


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


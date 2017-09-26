using TickerMonitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace TickerMonitor
{
    public class BitfinexOrderBook
    {
        public List<Bid> bids { get; set; }
        public List<Ask> asks { get; set; }
    }
}

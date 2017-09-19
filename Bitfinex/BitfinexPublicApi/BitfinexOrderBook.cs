using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices
{
    public class Bid
    {
        public string price { get; set; }
        public string amount { get; set; }
        public string timestamp { get; set; }
    }

    public class Ask
    {
        public string price { get; set; }
        public string amount { get; set; }
        public string timestamp { get; set; }
    }

    public class BitfinexOrderBook
    {
        public List<Bid> bids { get; set; }
        public List<Ask> asks { get; set; }
    }
}

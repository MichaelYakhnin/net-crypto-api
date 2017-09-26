using System;

namespace TickerMonitor
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
}

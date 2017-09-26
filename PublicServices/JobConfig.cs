using System;
using System.Collections.Generic;
using System.Text;

namespace TickerMonitor
{
    public class JobConfig
    {
        public string Asset { get; set; }
        public string Provider { get; set; }
        public int OrderNumber { get; set; }
        public string Url { get; set; }
        public int TimeJobTimer { get; set; }
    }
}

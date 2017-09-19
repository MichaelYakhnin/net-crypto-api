using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices
{
    
    public class OrderBooks
    {
        public OrderBooks(string _event, string _channel, string _pair,string _prec,string _freq,string _len)
        {
            Event = _event;
            channel = _channel;
            pair = _pair;
            prec = _prec;
            freq = _freq;
            len = _len;
        }

        public string Event { get; set; }
        public string channel { get; set; }
        public string pair { get; set; }
        public string prec { get; set; }
        public string freq { get; set; }
        public string len { get; set; }
    }
}

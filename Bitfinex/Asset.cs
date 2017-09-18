using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices
{
    public class Asset
    {
        public Asset(string _event,string _channel,string _pair)
        {
            Event = _event;
            channel = _channel;
            pair = _pair;
        }

        public string Event { get; set; }
        public string channel { get; set; }
        public string pair { get; set; }
    }
}

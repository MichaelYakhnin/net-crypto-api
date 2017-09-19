using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices
{
    public class LastDataOrderBook
    {
        public string Provider { get; set; }
        public string AssetName { get; set; }
        public Bid bids { get; set; }
        public Ask asks { get; set; }

        public LastDataOrderBook()
        {
            bids = new Bid();
            asks = new Ask();
        }

    }
}

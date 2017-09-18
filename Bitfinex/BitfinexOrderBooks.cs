using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices
{
    public class BitfinexOrderBooks : WebSocketApi
    {

        private const string uri = "wss://api.bitfinex.com/ws";
        private OrderBooks _ob;

        public BitfinexOrderBooks(OrderBooks ob)
        {
            _ob = ob;
            string _cmd = JsonConvert.SerializeObject(_ob);
            _cmd = _cmd.Replace("E", "e");
            cmd = _cmd;
            WebSocketApi.Connect(uri).Wait(); ;
        }
    }
}

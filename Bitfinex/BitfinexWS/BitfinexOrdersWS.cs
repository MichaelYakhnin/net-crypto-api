using Newtonsoft.Json;

namespace ExternalServices
{
    public class BitfinexOrdersrWS : WebSocketApi
    {
        private const string uri = "wss://api.bitfinex.com/ws";
        private BitstampOrderSend bitstampOrderSend;

        public BitfinexOrdersrWS(BitstampOrderSend _bitstampOrderSendt)
        {
            bitstampOrderSend = _bitstampOrderSendt;
            string _cmd = JsonConvert.SerializeObject(bitstampOrderSend);
            cmd = _cmd;
            Connect(uri).Wait(); ;
        }
    }
    
}


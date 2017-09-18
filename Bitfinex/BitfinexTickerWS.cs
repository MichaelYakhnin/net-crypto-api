using Newtonsoft.Json;

namespace ExternalServices
{
    public class BitfinexTickerWS : WebSocketApi
    {
        private const string uri = "wss://api.bitfinex.com/ws";
        private Asset _asset;

        public BitfinexTickerWS(Asset asset)
        {
             _asset = asset;
            string _cmd = JsonConvert.SerializeObject(_asset);
            _cmd = _cmd.Replace("E", "e");
            cmd = _cmd;
            WebSocketApi.Connect(uri).Wait(); ;
        }
    }
    
}

